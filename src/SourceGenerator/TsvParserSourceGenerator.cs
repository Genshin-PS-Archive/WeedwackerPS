using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

#nullable enable

namespace SourceGenerator
{
    [Generator]
    public class TsvParserSourceGenerator : ISourceGenerator
    {
        private const char ElementSeparator_ = '\t';

        private const string LineVar_ = "line";
        private const string TabIndexVar_ = "tabIndex";
        private const string StartIndexVar_ = "startIndex";

        private readonly TsvParserSyntaxReceiver _receiver = new();

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => _receiver);
        }

        public void Execute(GeneratorExecutionContext context)
        {
            HashSet<string> delegates = new();
            HashSet<string> usingDeclarations = new();
            foreach (TypeDeclarationSyntax typeDeclaration in _receiver.TypeDeclarations)
            {
                SemanticModel typeModel = context.Compilation.GetSemanticModel(typeDeclaration.SyntaxTree);
                INamedTypeSymbol? symbol = typeModel.GetDeclaredSymbol(typeDeclaration);
                if (symbol == null)
                    continue;

                if (symbol.IsAbstract)
                    continue;

                if (TryEmitDeserialize(context, symbol, out string parserSource))
                {
                    // Deserialize file
                    context.AddSource($"{GetContainingTypeName(symbol, '_')}.g.cs", parserSource);

                    // Add entry to type dictionary
                    usingDeclarations.Add(symbol.ContainingNamespace.ToString());
                    delegates.Add($"        [typeof({GetContainingTypeName(symbol)})] = (ref ReadOnlySpan<char> line) => {GetContainingTypeName(symbol, '_')}Parser.Deserialize(ref line)");
                }
            }

            context.AddSource("TsvParser.g.cs", CreateTsvParserSource(delegates, usingDeclarations));
        }

        private bool TryEmitDeserialize(GeneratorExecutionContext context, INamedTypeSymbol type, out string source)
        {
            source = string.Empty;

            try
            {
                source = EmitDeserialize(type);
                return true;
            }
            catch (Exception e)
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    new DiagnosticDescriptor("TSV1000", "Generation error", "{0}", "Source generation",
                        DiagnosticSeverity.Warning, true), Location.Create($"{type.Name}.g.cs", new TextSpan(), new LinePositionSpan()), e.Message));
            }

            return false;
        }

        private string EmitDeserialize(INamedTypeSymbol type)
        {
            StringBuilder sb = new();
            HashSet<string> usingDeclarations = new() { "System.Runtime.InteropServices" };

            EmitDeserialize(type, sb, usingDeclarations);

            return CreateTypeParserSource(type, sb, usingDeclarations);
        }

        private string CreateTypeParserSource(INamedTypeSymbol type, StringBuilder sb, HashSet<string> usingDeclarations)
        {
            string template = """
                              {0}namespace Weedwacker.GameServer.Data;

                              {1} class {2}Parser
                              {{
                                  public static object Deserialize(ref ReadOnlySpan<char> {3})
                                  {{
                              {4}   }}
                              }}
                              """;

            usingDeclarations.Remove("Weedwacker.GameServer.Data");
            string namespaces = string.Join("\r\n", usingDeclarations.Select(x => $"using {x};"));
            if (namespaces != string.Empty)
                namespaces += "\r\n\r\n";

            return string.Format(template, namespaces, type.DeclaredAccessibility.ToString().ToLower(), GetContainingTypeName(type, '_'), LineVar_, sb);
        }

        private string CreateTsvParserSource(HashSet<string> delegates, HashSet<string> usingDeclarations)
        {
            string template = """
                              {0}namespace {1};

                              public static partial class TsvParser
                              {{
                                  private static Dictionary<Type, DeserializeDelegate> Delegates = new()
                                  {{
                              {2}
                                  }};
                              }}
                              """;

            string namespaces = string.Join("\r\n", usingDeclarations.Select(x => $"using {x};"));
            if (namespaces != string.Empty)
                namespaces += "\r\n\r\n";

            return string.Format(template, namespaces, _receiver.TsvParsersNamespace, string.Join(",\r\n", delegates));
        }

        #region Deserialize type

        private void EmitDeserialize(INamedTypeSymbol type, StringBuilder sb, HashSet<string> usingDeclarations)
        {
            // Create local variables
            AppendLine(2, "ReadOnlySpan<char> memberValue;", sb);
            AppendLine(2, $"int {StartIndexVar_} = 0;", sb);
            AppendLine(2, $"int {TabIndexVar_};", sb);
            AppendLine(sb);

            // Deserialize type and its members
            EmitDeserializeType(type, "result", LineVar_, "memberValue", 2, sb, usingDeclarations);
            AppendLine(sb);

            // Return parsed object
            AppendLine(2, $"{LineVar_} = {StartIndexVar_} >= {LineVar_}.Length ? ReadOnlySpan<char>.Empty : {LineVar_}[{StartIndexVar_}..];", sb);
            AppendLine(2, "return result;", sb);
        }

        private int EmitDeserializeType(INamedTypeSymbol type, string targetVar, string lineVar, string memberVar, int indent, StringBuilder sb, HashSet<string> usingDeclarations)
        {
            usingDeclarations.Add(type.ContainingNamespace.ToString());
            AppendLine(indent, $"{GetContainingTypeName(type)} {targetVar} = new();", sb);
            AppendLine(sb);

            return EmitDeserializeMembers(type, string.Empty, targetVar, lineVar, memberVar, indent, sb, usingDeclarations);
        }

        #endregion

        #region Deserialize members

        private int EmitDeserializeMembers(INamedTypeSymbol type, string parentMemberName, string targetVar, string lineVar, string memberVar, int indent, StringBuilder sb, HashSet<string> usingDeclarations)
        {
            AttributeData? columnsAttribute = GetColumnsAttribute(type);
            if (columnsAttribute == null)
                throw new InvalidOperationException($"Type '{type.Name}' does not have a ColumnsAttribute.");

            // Deserialize base type
            int baseConsumed = 0;
            if (type.BaseType != null && type.BaseType.Name != "Object")
            {
                baseConsumed = EmitDeserializeMembers(type.BaseType, parentMemberName, targetVar, lineVar, memberVar, indent, sb, usingDeclarations);
                if (baseConsumed < 0)
                    return -1;
            }

            // Get column count
            int columnsCount = GetColumnsAttributeCount(columnsAttribute);
            if (columnsCount <= 0)
                return baseConsumed;

            if (baseConsumed > 0)
                AppendLine(sb);

            // Find and order all deserializable members
            Dictionary<int, ISymbol> memberLookup = new();
            foreach (ISymbol member in type.GetMembers())
            {
                AttributeData? columnIndexAttribute = GetColumnIndexAttribute(member);
                if (columnIndexAttribute != null)
                    memberLookup[GetColumnIndexAttributeIndex(columnIndexAttribute)] = member;
            }

            // Deserialize all marked members
            int consumed = 0;
            while (consumed < columnsCount)
            {
                if (!memberLookup.TryGetValue(consumed, out ISymbol member))
                {
                    EmitSkipConsume(lineVar, indent, sb);
                    consumed++;

                    if (consumed < columnsCount)
                        AppendLine(sb);

                    continue;
                }

                int memberConsumed = EmitDeserializeMember(member, parentMemberName, targetVar, lineVar, memberVar, indent, sb, usingDeclarations);
                if (memberConsumed < 0)
                    return -1;

                consumed += memberConsumed;

                if (consumed < columnsCount)
                    AppendLine(sb);
            }

            return baseConsumed + consumed;
        }

        private int EmitDeserializeMember(ISymbol member, string parentMemberName, string targetVar, string lineVar, string memberVar, int indent, StringBuilder sb, HashSet<string> usingDeclarations)
        {
            ITypeSymbol memberType = GetMemberType(member);
            bool isNullable = memberType.IsReferenceType;

            ITypeSymbol? underlyingType = GetUnderlyingType(memberType);
            if (underlyingType != null)
            {
                isNullable = true;
                memberType = underlyingType;
            }

            string memberName = string.IsNullOrEmpty(parentMemberName) ? member.Name : $"{parentMemberName}.{member.Name}";
            AppendLine(indent, $"// Deserialize member {memberName}", sb);

            int consumed;
            if (memberType.TypeKind == TypeKind.Array)
                consumed = EmitDeserializeArrayMember(member, memberName, targetVar, lineVar, memberVar, indent, sb, usingDeclarations);
            else if (memberType.Name == "Dictionary")
            {
                EmitConsume(ElementSeparator_, TabIndexVar_, StartIndexVar_, lineVar, memberVar, indent, sb);
                AppendLine(sb);

                consumed = EmitDeserializeDictionaryMember(member, memberName, $"{targetVar}.{member.Name}", memberVar, indent, sb, usingDeclarations);
            }
            else if (!IsPrimitive(memberType))
                consumed = EmitDeserializeReferenceMember((INamedTypeSymbol)memberType, memberName, $"{targetVar}.{member.Name}", lineVar, memberVar, indent, sb, usingDeclarations);
            else
            {
                EmitSetIndex(ElementSeparator_, TabIndexVar_, StartIndexVar_, lineVar, indent, sb);
                consumed = EmitDeserializeValueMember(memberType, isNullable, $"{targetVar}.{member.Name}", lineVar, StartIndexVar_, true, indent, sb, usingDeclarations);
                EmitSetStartIndex(TabIndexVar_, StartIndexVar_, indent, sb);
            }

            return consumed;
        }

        #endregion

        #region Deserialize values

        private int EmitDeserializeValueMember(ITypeSymbol memberType, bool isNullable, string targetVar, string lineVar, string startIndex, bool isSpan, int indent, StringBuilder sb, HashSet<string> usingDeclarations)
        {
            // HINT: Assume tab index to be set already

            switch (memberType.Name)
            {
                case nameof(Byte):
                case nameof(UInt16):
                case nameof(UInt32):
                case nameof(UInt64):
                case nameof(UIntPtr): return EmitSetUnsignedIntegerMember(memberType, isNullable, targetVar, lineVar, startIndex, isSpan, indent, sb);
                case nameof(SByte):
                case nameof(Int16):
                case nameof(Int32):
                case nameof(Int64):
                case nameof(IntPtr): return EmitSetSignedIntegerMember(memberType, isNullable, targetVar, lineVar, startIndex, isSpan, indent, sb);
                case nameof(Single):
                case nameof(Double):
                case nameof(Decimal): return EmitSetFloatingPointMember(memberType, isNullable, targetVar, lineVar, startIndex, isSpan, indent, sb);
                case nameof(Char): return EmitDeserializeCharMember(isNullable, targetVar, lineVar, startIndex, indent, sb);
                case nameof(String): return EmitDeserializeStringMember(targetVar, isNullable, lineVar, startIndex, isSpan, indent, sb);
                case nameof(Boolean): return EmitDeserializeBooleanMember(targetVar, lineVar, startIndex, indent, sb);

                default:
                    if (memberType.TypeKind == TypeKind.Enum)
                        return EmitDeserializeEnumMember((INamedTypeSymbol)memberType, isNullable, targetVar, lineVar, startIndex, isSpan, indent, sb, usingDeclarations);
                    break;
            }

            return 1;
        }

        private int EmitSetUnsignedIntegerMember(ITypeSymbol memberType, bool isNullable, string targetVar, string lineVar, string startIndex, bool isSpan, int indent, StringBuilder sb)
        {
            // HINT: Assume tab index to be set already

            if (isNullable)
            {
                if (isSpan)
                    AppendLine(indent, $"if ({TabIndexVar_} > {startIndex})", sb);
                else
                    AppendLine(indent, $"if ({lineVar}.Length > 0)", sb);
                indent++;
            }

            // Parse value to primitive type
            string parseValue = isSpan ? EmitElementSpan(TabIndexVar_, startIndex, lineVar) : lineVar;
            if (memberType.Name == nameof(UInt64))
                AppendLine(indent, $"{targetVar} = TsvParserSupport.UlongParseFast({parseValue});", sb);
            else
                AppendLine(indent, $"{targetVar} = ({memberType})TsvParserSupport.UlongParseFast({parseValue});", sb);

            return 1;
        }

        private int EmitSetSignedIntegerMember(ITypeSymbol memberType, bool isNullable, string targetVar, string lineVar, string startIndex, bool isSpan, int indent, StringBuilder sb)
        {
            // HINT: Assume tab index to be set already

            if (isNullable)
            {
                if (isSpan)
                    AppendLine(indent, $"if ({TabIndexVar_} > {startIndex})", sb);
                else
                    AppendLine(indent, $"if ({lineVar}.Length > 0)", sb);
                indent++;
            }

            // Parse value to primitive type
            string parseValue = isSpan ? EmitElementSpan(TabIndexVar_, startIndex, lineVar) : lineVar;
            if (memberType.Name == nameof(Int64))
                AppendLine(indent, $"{targetVar} = TsvParserSupport.LongParseFast({parseValue});", sb);
            else
                AppendLine(indent, $"{targetVar} = ({memberType})TsvParserSupport.LongParseFast({parseValue});", sb);

            return 1;
        }

        private int EmitSetFloatingPointMember(ITypeSymbol memberType, bool isNullable, string targetVar, string lineVar, string startIndex, bool isSpan, int indent, StringBuilder sb)
        {
            // HINT: Assume tab index to be set already

            if (isNullable)
            {
                if (isSpan)
                    AppendLine(indent, $"if ({TabIndexVar_} > {startIndex})", sb);
                else
                    AppendLine(indent, $"if ({lineVar}.Length > 0)", sb);
                indent++;
            }

            // Parse value to floating-point type
            string parseValue = isSpan ? EmitElementSpan(TabIndexVar_, startIndex, lineVar) : lineVar;
            if (memberType.Name == nameof(Single))
                AppendLine(indent, $"{targetVar} = TsvParserSupport.FloatParseFast({parseValue});", sb);
            else
                AppendLine(indent, $"{targetVar} = ({memberType})TsvParserSupport.FloatParseFast({parseValue});", sb);

            return 1;
        }

        private int EmitDeserializeCharMember(bool isNullable, string targetVar, string lineVar, string startIndex, int indent, StringBuilder sb)
        {
            // HINT: Assume tab index to be set already

            // Get character value
            if (isNullable)
            {
                AppendLine(indent, $"if ({lineVar}.Length > {startIndex})", sb);
                AppendLine(indent + 1, $"{targetVar} = {lineVar}[{startIndex}];", sb);
            }
            else
            {
                AppendLine(indent, $"{targetVar} = {lineVar}[{startIndex}];", sb);
            }

            return 1;
        }

        private int EmitDeserializeStringMember(string targetVar, bool isNullable, string lineVar, string startIndex, bool isSpan, int indent, StringBuilder sb)
        {
            // HINT: Assume tab index to be set already

            if (isNullable)
            {
                if (isSpan)
                    AppendLine(indent, $"if ({TabIndexVar_} > {startIndex})", sb);
                else
                    AppendLine(indent, $"if ({lineVar}.Length > 0)", sb);
                indent++;
            }

            // Convert ot string value
            string parseValue = isSpan ? EmitElementSpan(TabIndexVar_, startIndex, lineVar) : lineVar;
            AppendLine(indent, $"{targetVar} = new string({parseValue});", sb);

            return 1;
        }

        private int EmitDeserializeBooleanMember(string targetVar, string lineVar, string startIndex, int indent, StringBuilder sb)
        {
            // HINT: Assume tab index to be set already

            // Get boolean
            AppendLine(indent, $"{targetVar} = {TabIndexVar_} > {startIndex} && {lineVar}[{startIndex}] == '1';", sb);

            return 1;
        }

        private int EmitDeserializeEnumMember(INamedTypeSymbol memberType, bool isNullable, string targetVar, string lineVar, string startIndex, bool isSpan, int indent, StringBuilder sb, HashSet<string> usingDeclarations)
        {
            // HINT: Assume tab index to be set already

            usingDeclarations.Add(memberType.ContainingNamespace.ToString());

            if (isNullable)
            {
                AppendLine(indent, $"if ({TabIndexVar_} > {startIndex})", sb);
                indent++;
            }

            // Parse value to enum type
            string parseValue = isSpan ? EmitElementSpan(TabIndexVar_, startIndex, lineVar) : lineVar;
            AppendLine(indent, $"{targetVar} = ({GetContainingTypeName(memberType)})TsvParserSupport.UlongParseFast({parseValue});", sb);

            return 1;
        }

        private int EmitDeserializeReferenceMember(INamedTypeSymbol memberType, string memberName, string targetVar, string lineVar, string memberVar, int indent, StringBuilder sb, HashSet<string> usingDeclarations)
        {
            usingDeclarations.Add(memberType.ContainingNamespace.ToString());

            // Create reference object
            AppendLine(indent, $"{targetVar} = new {GetContainingTypeName(memberType)}();", sb);
            AppendLine(sb);

            // Consume values for reference object
            return EmitDeserializeMembers(memberType, memberName, targetVar, lineVar, memberVar, indent, sb, usingDeclarations);
        }

        #endregion

        #region Deserialize dictionaries

        private int EmitDeserializeDictionaryMember(ISymbol member, string memberName, string targetVar, string memberVar, int indent, StringBuilder sb, HashSet<string> usingDeclarations)
        {
            AttributeData? tsvDictionaryAttribute = GetTsvDictionaryAttribute(member);
            if (tsvDictionaryAttribute == null)
                throw new InvalidOperationException($"Member '{memberName}' requires a TsvDictionaryAttribute.");

            ITypeSymbol[] dictionaryTypes = GetDictionaryTypes(member);
            char entrySeparator = GetTsvDictionaryAttributeEntrySeparator(tsvDictionaryAttribute);
            char keySeparator = GetTsvDictionaryAttributeKvpSeparator(tsvDictionaryAttribute);

            string localElementCountVar = $"{GetLocalVarSuffixFromMember(memberName)}Count";
            string localStartIndexVar = $"{GetLocalVarSuffixFromMember(memberName)}Start";

            // Leave member null, if no parseable content exists
            AppendLine(indent, $"if ({memberVar}.Length > 0)", sb);
            AppendLine(indent, "{", sb);

            // Calculate element length from line
            AppendLine(indent + 1, $"int {localElementCountVar} = 0;", sb);
            AppendLine(indent + 1, $"foreach (char c in {memberVar})", sb);
            AppendLine(indent + 1, "{", sb);
            AppendLine(indent + 2, $"bool comp = c == '{entrySeparator}';", sb);
            AppendLine(indent + 2, $"unsafe {{ {localElementCountVar} += *(byte*)&comp; }}", sb);
            AppendLine(indent + 1, "}", sb);
            AppendLine(sb);

            // Create new dictionary
            usingDeclarations.Add(GetMemberType(member).ContainingNamespace.ToString());
            usingDeclarations.Add(dictionaryTypes[0].ContainingNamespace.ToString());
            usingDeclarations.Add(dictionaryTypes[1].ContainingNamespace.ToString());
            // Create local startIndex
            AppendLine(indent + 1, $"int {localStartIndexVar} = 0;", sb);
            AppendLine(indent + 1, $"{targetVar} = new Dictionary<{GetArrayElementTypeName(dictionaryTypes[0], false)},{GetArrayElementTypeName(dictionaryTypes[1], false)}>();", sb);
            AppendLine(sb);

            // Loop through n elements
            string keyValueMemberVar = $"{GetLocalVarSuffixFromMember(memberName)}Key";
            AppendLine(indent + 1, $"{GetArrayElementTypeName(dictionaryTypes[0], false)} {keyValueMemberVar};", sb);

            AppendLine(indent + 1, $"for (int i = 0; i < {localElementCountVar}; i++)", sb);
            AppendLine(indent + 1, "{", sb);

            // Parse key
            EmitSetIndex(keySeparator, TabIndexVar_, localStartIndexVar, memberVar, indent + 2, sb);
            EmitDeserializeValueMember(dictionaryTypes[0], GetUnderlyingType(dictionaryTypes[0]) != null, keyValueMemberVar, memberVar, localStartIndexVar, true, indent + 2, sb, usingDeclarations);
            EmitSetStartIndex(TabIndexVar_, localStartIndexVar, indent + 2, sb);
            AppendLine(sb);

            // Parse element
            EmitSetIndex(entrySeparator, TabIndexVar_, localStartIndexVar, memberVar, indent + 2, sb);
            EmitDeserializeValueMember(dictionaryTypes[1], GetUnderlyingType(dictionaryTypes[1]) != null, $"{targetVar}[{keyValueMemberVar}]", memberVar, localStartIndexVar, true, indent + 2, sb, usingDeclarations);
            EmitSetStartIndex(TabIndexVar_, localStartIndexVar, indent + 2, sb);

            AppendLine(indent + 1, "}", sb);
            AppendLine(sb);

            // Parse last key
            EmitSetIndex(keySeparator, TabIndexVar_, localStartIndexVar, memberVar, indent + 1, sb);
            EmitDeserializeValueMember(dictionaryTypes[0], GetUnderlyingType(dictionaryTypes[0]) != null, keyValueMemberVar, memberVar, localStartIndexVar, true, indent + 1, sb, usingDeclarations);
            EmitSetStartIndex(TabIndexVar_, localStartIndexVar, indent + 1, sb);
            AppendLine(sb);

            // Parse last element
            AppendLine(indent, $"{TabIndexVar_} = {memberVar}.Length;", sb);
            EmitDeserializeValueMember(dictionaryTypes[1], GetUnderlyingType(dictionaryTypes[1]) != null, $"{targetVar}[{keyValueMemberVar}]", memberVar, localStartIndexVar, true, indent + 1, sb, usingDeclarations);

            // No parseable content, end
            AppendLine(indent, "}", sb);

            return 1;
        }

        #endregion

        #region Deserialize arrays

        private int EmitDeserializeArrayMember(ISymbol member, string memberName, string targetVar, string lineVar, string memberVar, int indent, StringBuilder sb, HashSet<string> usingDeclarations)
        {
            // Get TsvArray information
            AttributeData? tsvArrayAttribute = GetTsvArrayAttribute(member);
            if (tsvArrayAttribute == null)
                throw new InvalidOperationException($"Member '{memberName}' does not have a TsvArrayAttribute.");

            int tsvArrayLength = GetTsvArrayAttributeLength(tsvArrayAttribute);
            char[] tsvArraySeparators = GetTsvArrayAttributeSeparators(tsvArrayAttribute);

            // Get element type of array
            ITypeSymbol? elementType = GetMemberArrayElementType(member);
            if (elementType == null)
                throw new InvalidOperationException($"Element type of member '{memberName}' could not be determined.");

            bool isNullable = elementType.IsReferenceType;

            ITypeSymbol? underlyingType = GetUnderlyingType(elementType);
            if (underlyingType != null)
            {
                isNullable = true;
                elementType = underlyingType;
            }

            // Create and fill array
            int consumed;
            if (tsvArrayLength >= 0)
                consumed = EmitDeserializeFixedLengthArray(elementType, isNullable, memberName, tsvArrayLength, tsvArraySeparators, $"{targetVar}.{member.Name}", lineVar, memberVar, indent, sb, usingDeclarations);
            else
                consumed = EmitDeserializeDynamicLengthArray(elementType, isNullable, memberName, tsvArraySeparators, $"{targetVar}.{member.Name}", lineVar, memberVar, indent, sb, usingDeclarations);

            return consumed;
        }

        private int EmitDeserializeFixedLengthArray(ITypeSymbol elementType, bool isNullable, string memberName, int length, char[] separators, string targetVar, string lineVar, string memberVar, int indent, StringBuilder sb, HashSet<string> usingDeclarations)
        {
            string localLineVar = lineVar;
            string localStartIndexVar = StartIndexVar_;

            bool isPrimitive = IsPrimitive(elementType);
            if (isPrimitive)
            {
                if (separators.Length > 0)
                {
                    if (separators.Contains(ElementSeparator_))
                        throw new InvalidOperationException($"TsvArrayAttribute of member '{memberName}' can not declare a '{ElementSeparator_}' as one of its separators.");

                    // Consume next value in line
                    EmitConsume(ElementSeparator_, TabIndexVar_, StartIndexVar_, lineVar, memberVar, indent, sb);

                    localLineVar = memberVar;
                }
                else
                    separators = new[] { ElementSeparator_ };
            }
            else
            {
                if (separators.Length > 0)
                    throw new InvalidOperationException($"TsvArrayAttribute of member '{memberName}' can not declare separators as arrays of reference types are only separated by '{ElementSeparator_}'.");
            }

            // Leave member null, if no parseable content exists
            if (isPrimitive && separators[0] != ElementSeparator_)
            {
                AppendLine(sb);
                AppendLine(indent, $"if ({localLineVar}.Length > 0)", sb);
                AppendLine(indent, "{", sb);
                indent++;
            }

            // Create local startIndex
            if (isPrimitive && separators[0] != ElementSeparator_)
            {
                localStartIndexVar = $"{GetLocalVarSuffixFromMember(memberName)}Start";
                AppendLine(indent, $"int {localStartIndexVar} = 0;", sb);
            }

            // Create array
            AppendLine(indent, $"{targetVar} = new {GetArrayElementTypeName(elementType, isNullable)}[{length}];", sb);
            if (length > 0)
                AppendLine(sb);

            // Add elements to array
            int totalConsumed = 0;
            for (int i = 0; i < length; i++)
            {
                int consumed;
                if (isPrimitive)
                {
                    if (i + 1 == length && separators[0] != ElementSeparator_)
                    {
                        AppendLine(indent, $"{TabIndexVar_} = {localLineVar}.Length;", sb);
                        consumed = EmitDeserializeValueMember(elementType, isNullable, $"{targetVar}[{i}]", localLineVar, localStartIndexVar, true, indent, sb, usingDeclarations);
                    }
                    else
                    {
                        EmitSetIndex(separators, TabIndexVar_, localStartIndexVar, localLineVar, indent, sb);
                        consumed = EmitDeserializeValueMember(elementType, isNullable, $"{targetVar}[{i}]", localLineVar, localStartIndexVar, true, indent, sb, usingDeclarations);
                        EmitSetStartIndex(TabIndexVar_, localStartIndexVar, indent, sb);
                    }
                }
                else
                {
                    consumed = EmitDeserializeReferenceMember((INamedTypeSymbol)elementType, $"{targetVar}[{i}]", $"{targetVar}[{i}]", localLineVar, memberVar, indent, sb, usingDeclarations);
                }

                if (consumed < 0)
                {
                    if (isPrimitive && separators[0] != ElementSeparator_)
                        AppendLine(indent - 1, "}", sb);
                    return -1;
                }

                totalConsumed += consumed;

                if (i + 1 < length)
                    AppendLine(sb);
            }

            if (isPrimitive && separators[0] != ElementSeparator_)
            {
                AppendLine(indent - 1, "}", sb);
                totalConsumed = 1;
            }

            return totalConsumed;
        }

        private int EmitDeserializeDynamicLengthArray(ITypeSymbol elementType, bool isNullable, string memberName, char[] separators, string targetVar, string lineVar, string memberVar, int indent, StringBuilder sb, HashSet<string> usingDeclarations)
        {
            string localElementCountVar = $"{GetLocalVarSuffixFromMember(memberName)}Count";
            string localStartIndexVar = $"{GetLocalVarSuffixFromMember(memberName)}Start";

            // Determine integrity and ensure proper value setup
            bool isPrimitive = IsPrimitive(elementType);
            if (isPrimitive)
            {
                if (separators.Length <= 0)
                    throw new InvalidOperationException($"TsvArrayAttribute of member '{memberName}' with no separators needs to declare a fixed length.");

                if (separators.Contains(ElementSeparator_))
                    throw new InvalidOperationException($"TsvArrayAttribute of member '{memberName}' can not declare a '{ElementSeparator_}' as one of its separators.");
            }
            else
                throw new InvalidOperationException($"TsvArrayAttribute of member '{memberName}' needs to declare a fixed length.");

            // Consume next value in line
            EmitConsume(ElementSeparator_, TabIndexVar_, StartIndexVar_, lineVar, memberVar, indent, sb);
            AppendLine(sb);

            // Leave member null, if no parseable content exists
            AppendLine(indent, $"if ({memberVar}.Length > 0)", sb);
            AppendLine(indent, "{", sb);

            // Calculate element length from line
            AppendLine(indent + 1, $"int {localElementCountVar} = 0;", sb);
            AppendLine(indent + 1, $"foreach (char c in {memberVar})", sb);
            AppendLine(indent + 1, "{", sb);
            if (separators.Length > 1)
                AppendLine(indent + 2, $"bool comp = {string.Join(" || ", separators.Select(s => $"c == '{s}'"))};", sb);
            else
                AppendLine(indent + 2, $"bool comp = c == '{separators[0]}';", sb);
            AppendLine(indent + 2, $"unsafe {{ {localElementCountVar} += *(byte*)&comp; }}", sb);
            AppendLine(indent + 1, "}", sb);
            AppendLine(sb);

            // Create local startIndex
            AppendLine(indent + 1, $"int {localStartIndexVar} = 0;", sb);

            // Create new array
            AppendLine(indent + 1, $"{targetVar} = new {GetArrayElementTypeName(elementType, isNullable)}[{localElementCountVar} + 1];", sb);
            AppendLine(sb);

            // Loop through n elements
            AppendLine(indent + 1, "int j;", sb);
            AppendLine(indent + 1, $"for (j = 0; j < {localElementCountVar}; j++)", sb);
            AppendLine(indent + 1, "{", sb);

            EmitSetIndex(separators, TabIndexVar_, localStartIndexVar, memberVar, indent + 2, sb);
            int consumed = EmitDeserializeValueMember(elementType, isNullable, $"{targetVar}[j]", memberVar, localStartIndexVar, true, indent + 2, sb, usingDeclarations);
            EmitSetStartIndex(TabIndexVar_, localStartIndexVar, indent + 3, sb);

            AppendLine(indent + 1, "}", sb);
            AppendLine(sb);

            AppendLine(indent + 1, $"{TabIndexVar_} = {memberVar}.Length;", sb);
            EmitDeserializeValueMember(elementType, isNullable, $"{targetVar}[j]", memberVar, localStartIndexVar, true, indent + 1, sb, usingDeclarations);

            // No parseable content, end
            AppendLine(indent, "}", sb);

            return consumed;
        }

        #endregion

        #region Support

        #region Members

        // TODO: Letter match replacement
        private static readonly Regex MemberArrayRegex = new(@"\[([a-z0-9]{0,3})\]");
        private string GetLocalVarSuffixFromMember(string memberName)
        {
            string result = memberName.Replace('.', '_');
            return MemberArrayRegex.Replace(result, match => $"_{match.Groups[1].Value}");
        }

        #endregion

        #region Types

        private ITypeSymbol? GetMemberArrayElementType(ISymbol member)
        {
            ITypeSymbol memberType = GetMemberType(member);
            return (memberType as IArrayTypeSymbol)?.ElementType;
        }

        private ITypeSymbol[] GetDictionaryTypes(ISymbol member)
        {
            ITypeSymbol memberType = GetMemberType(member);
            return ((INamedTypeSymbol)memberType).TypeArguments.ToArray();
        }

        private ITypeSymbol GetMemberType(ISymbol member)
        {
            if (member is IFieldSymbol field)
                return field.Type;

            return (member as IPropertySymbol)?.Type!;
        }

        private ITypeSymbol? GetUnderlyingType(ITypeSymbol type)
        {
            if (type is { IsReferenceType: false, NullableAnnotation: NullableAnnotation.Annotated } and INamedTypeSymbol namedType)
                return namedType.TypeArguments[0];

            return null;
        }

        private bool IsPrimitive(ITypeSymbol type)
        {
            return type.IsValueType || type.Name == nameof(String);
        }

        private string GetContainingTypeName(ISymbol type, char separator = '.')
        {
            string result = type.Name;

            while (type.ContainingType != null)
            {
                result = type.ContainingType.Name + separator + result;
                type = type.ContainingType;
            }

            return result;
        }

        private string GetArrayElementTypeName(ITypeSymbol type, bool isNullable)
        {
            string typeName = type.IsReferenceType || type.TypeKind == TypeKind.Enum
                ? GetContainingTypeName(type)
                : type.ToString();

            if (type.IsValueType && isNullable)
                typeName += "?";

            return typeName;
        }

        #endregion

        #region Consume

        private void EmitConsume(char separator, string indexVar, string startIndexVar, string lineVar, string memberVar, int indent, StringBuilder sb)
        {
            EmitSetIndex(separator, indexVar, startIndexVar, lineVar, indent, sb);
            EmitSetElementSpan(memberVar, indexVar, startIndexVar, lineVar, indent, sb);
            EmitSetStartIndex(indexVar, startIndexVar, indent, sb);
        }

        #region TabIndex

        private void EmitSetIndex(char separator, string indexVar, string startIndexVar, string lineVar, int indent, StringBuilder sb)
        {
            EmitSetValue(indexVar, EmitFindIndex(separator, startIndexVar, lineVar), indent, sb);
        }

        private void EmitSetIndex(char[] separators, string indexVar, string startIndexVar, string lineVar, int indent, StringBuilder sb)
        {
            EmitSetValue(indexVar, EmitFindIndex(separators, startIndexVar, lineVar), indent, sb);
        }

        private string EmitFindIndex(char separator, string startIndexVar, string lineVar)
        {
            return $"TsvParserSupport.IndexOf(ref MemoryMarshal.GetReference({lineVar}), '{separator}', {startIndexVar}, {lineVar}.Length - {startIndexVar})";
        }

        private string EmitFindIndex(char[] separators, string startIndexVar, string lineVar)
        {
            if (separators.Length == 1)
                return EmitFindIndex(separators[0], startIndexVar, lineVar);

            if (separators.Length == 2)
                return $"TsvParserSupport.IndexOfAny(ref MemoryMarshal.GetReference({lineVar}), '{separators[0]}', '{separators[1]}', {startIndexVar}, {lineVar}.Length - {startIndexVar})";

            if (separators.Length == 3)
                return $"TsvParserSupport.IndexOfAny(ref MemoryMarshal.GetReference({lineVar}), '{separators[0]}', '{separators[1]}', '{separators[2]}', {startIndexVar}, {lineVar}.Length - {startIndexVar})";

            throw new InvalidOperationException("No more than 3 separators are allowed for TsvArrayAttributes.");
        }

        #endregion

        #region Before TabIndex

        private void EmitSetElementSpan(string memberVar, string indexVar, string startIndexVar, string lineVar, int indent, StringBuilder sb)
        {
            EmitSetValue(memberVar, EmitElementSpan(indexVar, startIndexVar, lineVar), indent, sb);
        }

        private string EmitElementSpan(string indexVar, string startIndexVar, string lineVar)
        {
            return $"{lineVar}[{startIndexVar}..{indexVar}]";
        }

        #endregion

        #region After TabIndex

        private void EmitSetStartIndex(string indexVar, string startIndexVar, int indent, StringBuilder sb)
        {
            EmitSetValue(startIndexVar, $"{indexVar} + 1", indent, sb);
        }

        #endregion

        private void EmitSetValue(string setVar, string setExpression, int indent, StringBuilder sb)
        {
            AppendLine(indent, $"{setVar} = {setExpression};", sb);
        }

        private void EmitSkipConsume(string lineVar, int indent, StringBuilder sb)
        {
            EmitSetIndex(ElementSeparator_, TabIndexVar_, StartIndexVar_, lineVar, indent, sb);
            EmitSetStartIndex(TabIndexVar_, StartIndexVar_, indent, sb);
        }

        #endregion

        #region Attributes

        #region ColumnsAttribute

        private AttributeData? GetColumnsAttribute(INamedTypeSymbol type)
        {
            return GetAttribute(type, "ColumnsAttribute");
        }

        private int GetColumnsAttributeCount(AttributeData columnsAttribute)
        {
            return (int)(columnsAttribute.ConstructorArguments[0].Value ?? 0);
        }

        #endregion

        #region ColumnIndexAttribute

        private AttributeData? GetColumnIndexAttribute(ISymbol type)
        {
            return GetAttribute(type, "ColumnIndexAttribute");
        }

        private int GetColumnIndexAttributeIndex(AttributeData columnIndexAttribute)
        {
            return (int)(columnIndexAttribute.ConstructorArguments[0].Value ?? 0);
        }

        #endregion

        #region TsvArrayAttribute

        private AttributeData? GetTsvArrayAttribute(ISymbol type)
        {
            return GetAttribute(type, "TsvArrayAttribute");
        }

        private int GetTsvArrayAttributeLength(AttributeData tsvArrayAttribute)
        {
            TypedConstant lengthArgument = tsvArrayAttribute.ConstructorArguments.FirstOrDefault(x => x.Type?.Name == nameof(Int32));
            return (int)(lengthArgument.Value ?? -1);
        }

        private char[] GetTsvArrayAttributeSeparators(AttributeData tsvArrayAttribute)
        {
            IEnumerable<TypedConstant> separatorArguments = tsvArrayAttribute.ConstructorArguments.Where(x => x.Kind == TypedConstantKind.Array).SelectMany(x => x.Values);
            return separatorArguments.Select(x => (char)x.Value!).ToArray();
        }

        #endregion

        #region TsvDictionaryAttribute

        private AttributeData? GetTsvDictionaryAttribute(ISymbol type)
        {
            return GetAttribute(type, "TsvDictionaryAttribute");
        }

        private char GetTsvDictionaryAttributeKvpSeparator(AttributeData tsvArrayAttribute)
        {
            return (char)tsvArrayAttribute.ConstructorArguments[0].Value!;
        }

        private char GetTsvDictionaryAttributeEntrySeparator(AttributeData tsvArrayAttribute)
        {
            return (char)tsvArrayAttribute.ConstructorArguments[1].Value!;
        }

        #endregion

        private AttributeData? GetAttribute(ISymbol type, string attributeName)
        {
            return type.GetAttributes().FirstOrDefault(a => a.AttributeClass?.Name == attributeName);
        }

        #endregion

        #endregion

        #region Lines

        private void AppendLine(StringBuilder sb)
        {
            sb.AppendLine();
        }

        private void AppendLine(int indent, string line, StringBuilder sb)
        {
            sb.AppendLine(new string('\t', indent) + line);
        }

        #endregion
    }
}
