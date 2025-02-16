using System.Reflection;
using FluentAssertions;

// ReSharper disable UnusedMember.Local

namespace SoapOnAScope.UnitTests.Reflection;

public class AutoSanitizeAttributeTests
{
    [AutoSanitize]
    private class TestClassWithDefaults
    {
        public string? Property { get; set; }
    }

    [AutoSanitize(trim: false, html: true, url: true, js: true)]
    private class TestClassWithOppositeDefaults
    {
        public string? Property { get; set; }
    }

    [Fact]
    public void TestClass_decorated_with_AutoSanitize_should_have_default_values()
    {
        var testObject = new TestClassWithDefaults();
        var attribute = testObject.GetType().GetCustomAttribute<AutoSanitizeAttribute>();
        attribute
            .Should()
            .BeEquivalentTo(
                new AutoSanitizeAttribute(),
                "TypeId should match if the test class and equivalent assertion are the same"
            );
    }

    [Fact]
    public void TestClass_with_explicit_opposite_to_defaults_should_match()
    {
        var testObject = new TestClassWithOppositeDefaults();
        var attribute = testObject.GetType().GetCustomAttribute<AutoSanitizeAttribute>();
        attribute
            .Should()
            .BeEquivalentTo(
                new AutoSanitizeAttribute(trim: false, html: true, url: true, js: true),
                "TypeId should match if the test class and equivalent assertion are the same"
            );
    }
}
