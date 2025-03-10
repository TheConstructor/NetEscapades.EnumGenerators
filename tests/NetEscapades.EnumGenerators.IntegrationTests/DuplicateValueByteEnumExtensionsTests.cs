using System;
using FluentAssertions;
using Xunit;

#if INTEGRATION_TESTS
namespace NetEscapades.EnumGenerators.IntegrationTests;
#elif NETSTANDARD_INTEGRATION_TESTS
namespace NetEscapades.EnumGenerators.NetStandard.IntegrationTests;
#elif INTERCEPTOR_TESTS
namespace NetEscapades.EnumGenerators.Interceptors.IntegrationTests;
#elif NUGET_ATTRS_INTEGRATION_TESTS
namespace NetEscapades.EnumGenerators.Nuget.Attributes.IntegrationTests;
#elif NUGET_INTEGRATION_TESTS
namespace NetEscapades.EnumGenerators.Nuget.IntegrationTests;
#elif NUGET_INTERCEPTOR_TESTS
namespace NetEscapades.EnumGenerators.Nuget.Interceptors.IntegrationTests;
#else
#error Unknown integration tests
#endif

public class DuplicateValueByteEnumExtensionsTests : ExtensionTests<DuplicateValueByteEnum>
{
    public static TheoryData<DuplicateValueByteEnum> ValidEnumValues() => new()
    {
        DuplicateValueByteEnum.First,
        DuplicateValueByteEnum.Min,
        DuplicateValueByteEnum.Second,
        DuplicateValueByteEnum.Max,
    };

    public static TheoryData<string> ValuesToParse() => new()
    {
        "First",
        "Second",
        "2nd",
        "2ND",
        "first",
        "SECOND",
        "0",
        "00",
        "1",
        "01",
        "267",
        "-267",
        "2147483647",
        "3000000000",
        "Fourth",
        "Fifth",
        "Min",
        "Max",
        "min",
        "max",
    };

    protected override string ToStringFast(DuplicateValueByteEnum value) => value.ToStringFast();
    protected override string ToStringFast(DuplicateValueByteEnum value, bool withMetadata) => value.ToStringFast(withMetadata);
    protected override bool IsDefined(DuplicateValueByteEnum value) => DuplicateValueByteEnumExtensions.IsDefined(value);
    protected override bool IsDefined(string name, bool allowMatchingMetadataAttribute) => DuplicateValueByteEnumExtensions.IsDefined(name, allowMatchingMetadataAttribute: false);
#if READONLYSPAN
    protected override bool IsDefined(in ReadOnlySpan<char> name, bool allowMatchingMetadataAttribute) => DuplicateValueByteEnumExtensions.IsDefined(name, allowMatchingMetadataAttribute: false);
#endif
    protected override bool TryParse(string name, out DuplicateValueByteEnum parsed, bool ignoreCase, bool allowMatchingMetadataAttribute)
        => DuplicateValueByteEnumExtensions.TryParse(name, out parsed, ignoreCase);
#if READONLYSPAN
    protected override bool TryParse(in ReadOnlySpan<char> name, out DuplicateValueByteEnum parsed, bool ignoreCase, bool allowMatchingMetadataAttribute)
        => DuplicateValueByteEnumExtensions.TryParse(name, out parsed, ignoreCase);
#endif
    protected override DuplicateValueByteEnum Parse(string name, bool ignoreCase, bool allowMatchingMetadataAttribute)
        => DuplicateValueByteEnumExtensions.Parse(name, ignoreCase);
#if READONLYSPAN
    protected override DuplicateValueByteEnum Parse(in ReadOnlySpan<char> name, bool ignoreCase, bool allowMatchingMetadataAttribute)
        => DuplicateValueByteEnumExtensions.Parse(name, ignoreCase);
#endif

    [Theory]
    [MemberData(nameof(ValidEnumValues))]
#if NET7_OR_GREATER
    public void GeneratesToStringFast(DuplicateValueByteEnum value) => GeneratesToStringFastTest(value);
#else
    // Before .NET 7 .ToString() doesn't always return the first Name for each underlying value
    public void GeneratesToStringFast(DuplicateValueByteEnum value)
    {
        var serialized = ToStringFast(value);
        var values = (DuplicateValueByteEnum[]) Enum.GetValues(typeof(DuplicateValueByteEnum));
        var names = Enum.GetNames(typeof(DuplicateValueByteEnum));
        var expectedValue = names[Array.IndexOf(values, value)];

        serialized.Should().Be(expectedValue);

        var serializedAltPath = ToStringFast(value, withMetadata: false);
        serializedAltPath.Should().Be(expectedValue);
    }
#endif

    [Theory]
    [MemberData(nameof(ValidEnumValues))]
#if NET7_OR_GREATER
    public void GeneratesToStringFastWithMetadata(DuplicateValueByteEnum value) => GeneratesToStringFastWithMetadataTest(value);
#else
    // Before .NET 7 .ToString() doesn't always return the first Name for each underlying value
    public void GeneratesToStringFastWithMetadata(DuplicateValueByteEnum value)
    {
        var serialized = ToStringFast(value, withMetadata: true);
        var values = (DuplicateValueByteEnum[]) Enum.GetValues(typeof(DuplicateValueByteEnum));
        var names = Enum.GetNames(typeof(DuplicateValueByteEnum));
        var valueAsString = names[Array.IndexOf(values, value)];

        var expectedValue = TryGetDisplayNameOrDescription(valueAsString, out var displayName)
            ? displayName : valueAsString;
        
        serialized.Should().Be(expectedValue);
    }
#endif

    [Theory]
    [MemberData(nameof(ValidEnumValues))]
    public void GeneratesIsDefined(DuplicateValueByteEnum value) => GeneratesIsDefinedTest(value);

    [Theory]
    [MemberData(nameof(ValuesToParse))]
    public void GeneratesIsDefinedUsingName(string name) => GeneratesIsDefinedTest(name, allowMatchingMetadataAttribute: false);

#if READONLYSPAN
    [Theory]
    [MemberData(nameof(ValuesToParse))]
    public void GeneratesIsDefinedUsingNameAsSpan(string name) => GeneratesIsDefinedTest(name.AsSpan(), allowMatchingMetadataAttribute: false);
#endif

    [Theory]
    [MemberData(nameof(ValuesToParse))]
    public void GeneratesTryParse(string name) => GeneratesTryParseTest(name, ignoreCase:false, allowMatchingMetadataAttribute: false);

#if READONLYSPAN
    [Theory]
    [MemberData(nameof(ValuesToParse))]
    public void GeneratesTryParseAsSpan(string name) => GeneratesTryParseTest(name.AsSpan(), ignoreCase: false, allowMatchingMetadataAttribute: false);
#endif

    [Theory]
    [MemberData(nameof(ValuesToParse))]
    public void GeneratesTryParseIgnoreCase(string name) => GeneratesTryParseTest(name, ignoreCase: true, allowMatchingMetadataAttribute: false);

#if READONLYSPAN
    [Theory]
    [MemberData(nameof(ValuesToParse))]
    public void GeneratesTryParseIgnoreCaseAsAspan(string name) => GeneratesTryParseTest(name.AsSpan(), ignoreCase: true, allowMatchingMetadataAttribute: false);
#endif

    [Fact]
    public void GeneratesGetValues() => GeneratesGetValuesTest(DuplicateValueByteEnumExtensions.GetValues());

    [Fact]
    public void GeneratesGetNames() => base.GeneratesGetNamesTest(DuplicateValueByteEnumExtensions.GetNames());
}