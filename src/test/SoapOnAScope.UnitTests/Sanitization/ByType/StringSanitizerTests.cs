using FluentAssertions;
using SoapOnAScope.UnitTests.TestModels;

namespace SoapOnAScope.UnitTests.Sanitization.ByType;

public class StringSanitizerTests
{
    // [Theory]
    // [InlineData(StringTestCases.Trim., "TRIM ME")]
    // [InlineData(HtmlExample, "<div>EncodeMe</div>")]
    // [InlineData(TrimAndHtmlExample, "<div>EncodeMe</div>")]
    // public void PureSanitize_WithTrimOnly_ShouldReturnTrimmedString(string input, string expected)
    // {
    //     var sut = new StringSanitizer(new SanitizationSpecification(Trim: true));
    //     var inputCopy = input;
    //     var res = sut.PureSanitize(input);
    //     res.Should().Be(expected);
    //     input.Should().Be(inputCopy);
    // }
    //
    // [Theory]
    // [InlineData(TrimExample, "TRIM ME")]
    // [InlineData(HtmlExample, "<div>EncodeMe</div>")]
    // [InlineData(TrimAndHtmlExample, "<div>EncodeMe</div>")]
    // public void PureSanitize_WithHTMLOnly_Should(string input, string expected)
    // {
    //     var sut = new StringSanitizer(new SanitizationSpecification(HtmlEncode: true));
    //     var inputCopy = input;
    //     var res = sut.PureSanitize(input);
    //     res.Should().Be(expected);
    //     input.Should().Be(inputCopy);
    // }
}
