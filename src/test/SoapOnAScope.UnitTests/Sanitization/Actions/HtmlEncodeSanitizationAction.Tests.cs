using FluentAssertions;
using SoapOnAScope.UnitTests.TestModels;

namespace SoapOnAScope.UnitTests.Sanitization.Actions;

public class HtmlEncodeSanitizationActionTests
{
    [Theory]
    [InlineData(StringTestCases.Html.BaseString, StringTestCases.Html.BaseString)]
    [InlineData(StringTestCases.Html.EncodedString, StringTestCases.Html.EncodedString)]
    public void Perform_should_do_nothing_if_predicate_returns_false(
        string input,
        string expectedOutput
    )
    {
        var sut = new HtmlEncodeSanitizationAction(_ => false);
        sut.Perform(input).Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData(StringTestCases.Html.BaseString, StringTestCases.Html.EncodedString)]
    public void Perform_should_encode_HTML_if_predicate_true(string input, string expectedOutput)
    {
        var sut = new HtmlEncodeSanitizationAction(_ => true);
        sut.Perform(input).Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData(StringTestCases.Empty.Blank, StringTestCases.Empty.Blank)]
    [InlineData(StringTestCases.Empty.Null, StringTestCases.Empty.Null)]
    [InlineData(StringTestCases.Empty.Spaces, StringTestCases.Empty.Spaces)]
    [InlineData(StringTestCases.Empty.NewLine, StringTestCases.Empty.NewLine)]
    public void Perform_should_handle_null_and_empty_strings(string? input, string? expectedOutput)
    {
        var sut = new HtmlEncodeSanitizationAction(_ => true);
        sut.Perform(input).Should().Be(expectedOutput);
    }
}
