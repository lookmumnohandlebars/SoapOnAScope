using FluentAssertions;
using SoapOnAScope.UnitTests.TestModels;

namespace SoapOnAScope.UnitTests.Sanitization;

public partial class SanitizerTests
{
    [Fact]
    public void Sanitize_with_class_using_auto_sanitize_should_auto_sanitize()
    {
        var obj = new AutoSanitizeComplexClass();
        Sanitizer.Sanitize(obj);
        obj.TrimString.Should().BeEquivalentTo(StringTestCases.Trim.BaseString);
    }

    [Fact]
    public void Sanitize_class_with_enumerables_should_sanitize_strings()
    {
        var obj = new EnumerablesClass();
        Sanitizer.Sanitize(obj);
        obj.StringsToTrim.First().Should().Be("TRIM ME");
    }
}
