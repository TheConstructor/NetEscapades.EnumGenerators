using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
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

public class DuplicateValueByteFlagsEnumExtensionsTests : ExtensionTests<DuplicateValueByteFlagsEnum>
{
    public static TheoryData<DuplicateValueByteFlagsEnum> ValidEnumValues() => new()
    {
        DuplicateValueByteFlagsEnum.First,
        DuplicateValueByteFlagsEnum.Second,
        DuplicateValueByteFlagsEnum.FirstAndSecond,
        (DuplicateValueByteFlagsEnum)6,
        (DuplicateValueByteFlagsEnum)7,
        (DuplicateValueByteFlagsEnum)9,
    };

    public static TheoryData<string> ValuesToParse() => new()
    {
        "First",
        "Second",
        "2nd",
        "2ND",
        "first",
        "SECOND",
        "3",
        "267",
        "-267",
        "2147483647",
        "3000000000",
        "Fourth",
        "Fifth",
    };

    protected override string ToStringFast(DuplicateValueByteFlagsEnum value) => value.ToStringFast();
    protected override string ToStringFast(DuplicateValueByteFlagsEnum value, bool withMetadata) => value.ToStringFast(withMetadata);
    protected override bool IsDefined(DuplicateValueByteFlagsEnum value) => DuplicateValueByteFlagsEnumExtensions.IsDefined(value);
    protected override bool IsDefined(string name, bool allowMatchingMetadataAttribute) => DuplicateValueByteFlagsEnumExtensions.IsDefined(name, allowMatchingMetadataAttribute: false);
#if READONLYSPAN
    protected override bool IsDefined(in ReadOnlySpan<char> name, bool allowMatchingMetadataAttribute) => DuplicateValueByteFlagsEnumExtensions.IsDefined(name, allowMatchingMetadataAttribute: false);
#endif
    protected override bool TryParse(string name, out DuplicateValueByteFlagsEnum parsed, bool ignoreCase, bool allowMatchingMetadataAttribute)
        => DuplicateValueByteFlagsEnumExtensions.TryParse(name, out parsed, ignoreCase);
#if READONLYSPAN
    protected override bool TryParse(in ReadOnlySpan<char> name, out DuplicateValueByteFlagsEnum parsed, bool ignoreCase, bool allowMatchingMetadataAttribute)
        => DuplicateValueByteFlagsEnumExtensions.TryParse(name, out parsed, ignoreCase);
#endif
    protected override DuplicateValueByteFlagsEnum Parse(string name, bool ignoreCase, bool allowMatchingMetadataAttribute)
        => DuplicateValueByteFlagsEnumExtensions.Parse(name, ignoreCase);
#if READONLYSPAN
    protected override DuplicateValueByteFlagsEnum Parse(in ReadOnlySpan<char> name, bool ignoreCase, bool allowMatchingMetadataAttribute)
        => DuplicateValueByteFlagsEnumExtensions.Parse(name, ignoreCase);
#endif

    [Theory]
    [MemberData(nameof(ValidEnumValues))]
    public void GeneratesToStringFast(DuplicateValueByteFlagsEnum value)
#if NET8_OR_GREATER
        => GeneratesToStringFastTest(value);
#else
    {
        // Before .NET 8 the name used in ToString for a given underlying value varies -> we compare by underlying value
        var serialized = ToStringFast(value)
            .Split(',').Select(s => s.Trim())
            .Select(n => (DuplicateValueByteFlagsEnum) Enum.Parse(typeof(DuplicateValueByteFlagsEnum), n))
            .ToList();
        var expectedValue = value.ToString()
            .Split(',').Select(s => s.Trim())
            .Select(n => (DuplicateValueByteFlagsEnum) Enum.Parse(typeof(DuplicateValueByteFlagsEnum), n))
            .ToList();

        serialized.Should().Equal(expectedValue);

        var serializedAltPath = ToStringFast(value, withMetadata: false)
            .Split(',').Select(s => s.Trim())
            .Select(n => (DuplicateValueByteFlagsEnum) Enum.Parse(typeof(DuplicateValueByteFlagsEnum), n))
            .ToList();
        serializedAltPath.Should().Equal(expectedValue);
    }
#endif

    [Theory]
    [MemberData(nameof(ValidEnumValues))]
    public void GeneratesToStringFastWithMetadata(DuplicateValueByteFlagsEnum value)
#if NET8_OR_GREATER
        => GeneratesToStringFastWithMetadataTest(value);
#else
    {
        // Before .NET 8 the name used in ToString for a given underlying value varies -> we compare by underlying value
        var serialized = ToStringFast(value, withMetadata: true)
            .Split(',').Select(s => s.Trim())
            // We don't have DisplayNames in DuplicateValueByteFlagsEnum... lucky us
            .Select(n => (DuplicateValueByteFlagsEnum) Enum.Parse(typeof(DuplicateValueByteFlagsEnum), n))
            .ToList();
        var expectedValue = value.ToString()
            .Split(',').Select(s => s.Trim())
            .Select(n => (DuplicateValueByteFlagsEnum) Enum.Parse(typeof(DuplicateValueByteFlagsEnum), n))
            .ToList();
        
        serialized.Should().Equal(expectedValue);
    }
#endif

    [Theory]
    [MemberData(nameof(ValidEnumValues))]
    public void GeneratesIsDefined(DuplicateValueByteFlagsEnum value) => GeneratesIsDefinedTest(value);

    [Theory]
    [MemberData(nameof(ValuesToParse))]
    public void GeneratesIsDefinedUsingName(string name) => GeneratesIsDefinedTest(name, allowMatchingMetadataAttribute: false);

#if READONLYSPAN
    [Theory]
    [MemberData(nameof(ValuesToParse))]
    public void GeneratesIsDefinedUsingNameAsSpan(string name) => GeneratesIsDefinedTest(name.AsSpan(), allowMatchingMetadataAttribute: false);
#endif

    public static IEnumerable<object[]> AllFlags()
    {
        var values = new[]
        {
            DuplicateValueByteFlagsEnum.First,
            DuplicateValueByteFlagsEnum.Second,
            DuplicateValueByteFlagsEnum.Third,
            DuplicateValueByteFlagsEnum.FirstAndSecond,
            DuplicateValueByteFlagsEnum.First | DuplicateValueByteFlagsEnum.Third,
            (DuplicateValueByteFlagsEnum)65,
            (DuplicateValueByteFlagsEnum)0,
        };

        return from v1 in values
            from v2 in values
            select new object[] { v1, v2 };
    }
    
    [Theory]
    [MemberData(nameof(AllFlags))]
    public void HasFlags(DuplicateValueByteFlagsEnum value, DuplicateValueByteFlagsEnum flag)
    {
        var hasFlag = value.HasFlagFast(flag);

        hasFlag.Should().Be(value.HasFlag(flag));
    }

    [Theory]
    [MemberData(nameof(ValuesToParse))]
    public void GeneratesTryParse(string name) => GeneratesTryParseTest(name, ignoreCase: false, allowMatchingMetadataAttribute: false);

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
    public void GeneratesTryParseIgnoreCaseAsSpan(string name) => GeneratesTryParseTest(name.AsSpan(), ignoreCase: true, allowMatchingMetadataAttribute: false);
#endif

    [Theory]
    [MemberData(nameof(ValidEnumValues))]
    public void GeneratesAsUnderlyingType(DuplicateValueByteFlagsEnum value) => GeneratesAsUnderlyingTypeTest(value, value.AsUnderlyingType());

    [Fact]
    public void GeneratesGetValues() => GeneratesGetValuesTest(DuplicateValueByteFlagsEnumExtensions.GetValues());

    [Fact]
    public void GeneratesGetValuesAsUnderlyingType() => GeneratesGetValuesAsUnderlyingTypeTest(DuplicateValueByteFlagsEnumExtensions.GetValuesAsUnderlyingType());

    [Fact]
    public void GeneratesGetNames()
#if NET8_OR_GREATER
        => base.GeneratesGetNamesTest(DuplicateValueByteFlagsEnumExtensions.GetNames());
#else
    {
        // Before .NET 8 the order of names for the same underlying value seem to vary -> we check, if the names match the values
        var names = DuplicateValueByteFlagsEnumExtensions.GetNames();
        names.Select(n => (DuplicateValueByteFlagsEnum) Enum.Parse(typeof(DuplicateValueByteFlagsEnum), n))
            .Should()
            .Equal((DuplicateValueByteFlagsEnum[]) Enum.GetValues(typeof(DuplicateValueByteFlagsEnum)));

    }
#endif
}