using FluentAssertions;
using SoapOnAScope.Reflection.Models;
using SoapOnAScope.UnitTests.TestModels;

namespace SoapOnAScope.UnitTests.Sanitization;

public partial class SanitizerTests
{
    [Theory]
    [InlineData(StringTestCases.Trim.NeedsTrimming, StringTestCases.Trim.BaseString)]
    public void Sanitize_with_string_parameter_and_trim_defined_trims(string? input, string? expected)
    {
        Sanitizer.SanitizeString(ref input, new SanitizationMetaData(trim: true));
        input.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void SanitizString_with_string_parameter_and_default_specs_handles_null()
    {
        string? input = null;
        Sanitizer.SanitizeString(ref input, new SanitizationMetaData());
        input.Should().BeEquivalentTo(null);
    }
}
