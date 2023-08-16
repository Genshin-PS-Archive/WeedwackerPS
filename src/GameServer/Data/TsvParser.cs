namespace Weedwacker.GameServer.Data;

public static partial class TsvParser
{
    public delegate object DeserializeDelegate(ref ReadOnlySpan<char> line);

    public static bool CanDeserialize<T>()
    {
        return CanDeserialize(typeof(T));
    }

    public static bool CanDeserialize(Type type)
    {
        return TsvParser.Delegates.ContainsKey(type);
    }

    public static T DeserializeString<T>(string line)
    {
        ReadOnlySpan<char> span = line.AsSpan();
        return (T)DeserializeSpan(ref span, typeof(T));
    }

    public static T DeserializeSpan<T>(ref ReadOnlySpan<char> line)
    {
        return (T)DeserializeSpan(ref line, typeof(T));
    }

    public static object DeserializeString(string line, Type type)
    {
        ReadOnlySpan<char> span = line.AsSpan();
        return DeserializeSpan(ref span, type);
    }

    public static object DeserializeSpan(ref ReadOnlySpan<char> span, Type type)
    {
        if (!TsvParser.Delegates.TryGetValue(type, out DeserializeDelegate? del))
            throw new InvalidOperationException($"No deserialization method found for type {type.Name}.");
        
        return del(ref span);
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class ColumnsAttribute : Attribute
{
    /// <summary>
    /// The total number of columns in the given type.
    /// </summary>
    public readonly int Count;

    public ColumnsAttribute(int count)
    {
        Count = count;
    }
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ColumnIndexAttribute : Attribute
{
    /// <summary>
    /// The column index in the TSV for this member.
    /// </summary>
    public readonly int Index;

    public ColumnIndexAttribute(int index)
    {
        Index = index;
    }
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class TsvArrayAttribute : Attribute
{
    /// <summary>
    /// The length of the array. -1 for dynamic lengths.
    /// </summary>
    public readonly int Length;

    /// <summary>
    /// The value separators for this array.
    /// </summary>
    public readonly char[] Separators;

    public TsvArrayAttribute(int length) : this(length, Array.Empty<char>())
    { }

    public TsvArrayAttribute(params char[] separators) : this(-1, separators)
    { }

    public TsvArrayAttribute(int length, params char[] separator)
    {
        Length = length;
        Separators = separator;
    }
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class TsvDictionaryAttribute : Attribute
{
    /// <summary>
    /// The separator for key value pairs.
    /// </summary>
    public readonly char KvpSeparator;

    /// <summary>
    /// The separator for entries.
    /// </summary>
    public readonly char EntrySeparator;

    public TsvDictionaryAttribute(char kvpSeparator, char entrySeparator)
    {
        KvpSeparator = kvpSeparator;
        EntrySeparator = entrySeparator;
    }
}