using FluentAssertions;
using SoapOnAScope.UnitTests.TestModels;

namespace SoapOnAScope.UnitTests.Sanitization.Actions;

public class TrimSanitizationActionTests
{
    [Theory]
    [InlineData(StringTestCases.Trim.NeedsTrimming, StringTestCases.Trim.NeedsTrimming)]
    [InlineData(StringTestCases.Empty.Spaces, StringTestCases.Empty.Spaces)]
    [InlineData(StringTestCases.Empty.Null, StringTestCases.Empty.Null)]
    public void Sanitize_if_predicate_is_false_does_not_trim(string? input, string? expected)
    {
        var sut = new TrimSanitizationAction(_ => false);
        var res = sut.Perform(input!);
        res.Should().Be(expected);
    }

    [Theory]
    [InlineData(StringTestCases.Trim.AlreadyTrimmed, StringTestCases.Trim.BaseString)]
    [InlineData(StringTestCases.Trim.NeedsTrimming, StringTestCases.Trim.BaseString)]
    public void Sanitize_if_predicate_is_true_trims_input_strings(string? input, string? expected)
    {
        var sut = new TrimSanitizationAction(_ => true);
        var res = sut.Perform(input!);
        res.Should().Be(expected);
    }

    [Theory]
    [InlineData(StringTestCases.Empty.Null, StringTestCases.Empty.Null)]
    [InlineData(StringTestCases.Empty.Blank, StringTestCases.Empty.Blank)]
    [InlineData(StringTestCases.Empty.Spaces, StringTestCases.Empty.Blank)]
    [InlineData(StringTestCases.Empty.NewLine, StringTestCases.Empty.Blank)]
    public void Sanitize_if_predicate_is_true_handles_empty_and_null_strings(string? input, string? expected)
    {
        var sut = new TrimSanitizationAction(_ => true);
        var res = sut.Perform(input);
        res.Should().Be(expected);
    }
}
