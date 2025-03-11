using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace NetEscapades.EnumGenerators;

public readonly record struct EnumToGenerate
{
    public readonly string Name;
    public readonly string FullyQualifiedName;
    public readonly string Namespace;
    public readonly bool IsPublic;
    public readonly bool HasFlags;
    public readonly string UnderlyingType;

    /// <summary>
    /// Key is the enum name.
    /// </summary>
    public readonly EquatableArray<(string Key, EnumValueOption Value)> Names;

    public readonly bool IsDisplayAttributeUsed;

    public EnumToGenerate(
        string name,
        string ns,
        string fullyQualifiedName,
        string underlyingType,
        bool isPublic,
        List<(string Key, EnumValueOption Value)> names,
        bool hasFlags,
        bool isDisplayAttributeUsed)
    {
        Name = name;
        Namespace = ns;
        UnderlyingType = underlyingType;
        Names = new EquatableArray<(string Key, EnumValueOption Value)>(
            hasFlags
                ? names.Select((n,i) => (n,i))
                    .OrderBy(t => t.n.Value.ConstantValue)
                    .ThenBy(t => t.i)
                    .Select(t => t.n)
                    .ToArray()
                : names.ToArray());
        HasFlags = hasFlags;
        IsPublic = isPublic;
        FullyQualifiedName = fullyQualifiedName;
        IsDisplayAttributeUsed = isDisplayAttributeUsed;
    }
}