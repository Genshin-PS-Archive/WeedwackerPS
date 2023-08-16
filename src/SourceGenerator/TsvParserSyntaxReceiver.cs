#nullable enable
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace SourceGenerator
{
    class TsvParserSyntaxReceiver : ISyntaxReceiver
    {
        public readonly List<TypeDeclarationSyntax> TypeDeclarations = new();
        public string? TsvParsersNamespace;

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            // Ignore everything that isn't a TypeDeclaration
            if (syntaxNode is not TypeDeclarationSyntax typeDeclaration)
                return;

            if (typeDeclaration.Identifier.Text == "TsvParser")
            {
                TsvParsersNamespace = typeDeclaration.Ancestors().OfType<BaseNamespaceDeclarationSyntax>().FirstOrDefault()?.Name.ToString();
                return;
            }

            // Ignore types without [Columns]
            if (!HasColumnsAttribute(typeDeclaration))
                return;

            TypeDeclarations.Add(typeDeclaration);
        }

        private bool HasColumnsAttribute(TypeDeclarationSyntax typeDeclaration)
        {
            return typeDeclaration.AttributeLists.Any(als => als.Attributes.Any(a =>
            {
                string name = a.Name.ToString();
                return name is "Columns" or "ColumnsAttribute";
            }));
        }
    }
}
