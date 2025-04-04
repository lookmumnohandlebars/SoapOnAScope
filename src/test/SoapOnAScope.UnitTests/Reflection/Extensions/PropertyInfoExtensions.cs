using System.Reflection;
using FluentAssertions;

namespace SoapOnAScope.UnitTests.Reflection.Extensions;

public class PropertyInfoExtensions
{
    public class PropertyInfoMotherClass
    {
        [EncodeHtml]
        public string? HtmlEncodedString { get; set; }
        public string? NonEncodedString { get; set; }
    }
    
    private PropertyInfo _testPropertyInfo;

    public PropertyInfoExtensions()
    {
        _testPropertyInfo = new PropertyInfoMotherClass().GetType().GetProperty("HtmlEncodedString")!;
    }
    
    [Fact]
    public void PropertyInfo_Should_Have_EncodeHtmlAttribute()
    {
        var attribute = _testPropertyInfo.GetSanitizationAttribute<EncodeHtmlAttribute>();
        attribute.Exists.Should().BeTrue();
        attribute.SanitizationAttribute.Should().BeOfType<EncodeHtmlAttribute>().And.NotBeNull();
    }
}