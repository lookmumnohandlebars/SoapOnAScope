using System.Reflection;
using FluentAssertions;

// ReSharper disable UnusedMember.Local

namespace SoapOnAScope.UnitTests.Reflection;

public class SanitizeAttributeTests
{
    private class TestClassWithDefaults
    {
        [Sanitize]
        public string Property { get; set; } = "";
    }

    private class TestClassWithOppositeDefaults
    {
        [Sanitize(trim: false, html: true, url: true, js: true)]
        public string? Property { get; set; }
    }

    [Fact]
    public void TestClass_decorated_with_AutoSanitize_should_have_default_values()
    {
        var testObject = new TestClassWithDefaults();
        var attribute = testObject
            .GetType()
            .GetProperty("Property")!
            .GetCustomAttribute<SanitizeAttribute>();
        attribute
            .Should()
            .BeEquivalentTo(
                new SanitizeAttribute(),
                "TypeId should match if the test class and equivalent assertion are the same"
            );
    }

    [Fact]
    public void TestClass_with_explicit_opposite_to_defaults_should_match()
    {
        var testObject = new TestClassWithOppositeDefaults();
        var attribute = testObject
            .GetType()
            .GetProperty("Property")!
            .GetCustomAttribute<SanitizeAttribute>();
        attribute
            .Should()
            .BeEquivalentTo(
                new SanitizeAttribute(trim: false, html: true, url: true, js: true),
                "TypeId should match if the test class and equivalent assertion are the same"
            );
    }
}
