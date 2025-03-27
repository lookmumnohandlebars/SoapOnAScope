using FluentAssertions;
using SoapOnAScope.UnitTests.TestModels;

namespace SoapOnAScope.UnitTests.Sanitization;

public partial class SanitizerTests
{
    [Theory]
    [InlineData(StringTestCases.Trim.NeedsTrimming, StringTestCases.Trim.BaseString)]
    public void Sanitize_with_string_parameter_and_default_specs_trims(string? input, string? expected)
    {
        Sanitizer.Sanitize(ref input);
        input.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Sanitize_with_string_parameter_and_default_specs_handles_null()
    {
        string? input = null;
        Sanitizer.Sanitize(ref input);
        input.Should().BeEquivalentTo(null);
    }
}
