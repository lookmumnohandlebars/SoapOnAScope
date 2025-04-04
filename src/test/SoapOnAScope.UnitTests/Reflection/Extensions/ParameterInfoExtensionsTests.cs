using System.Reflection;
using FluentAssertions;
using SoapOnAScope.Reflection.Models;

namespace SoapOnAScope.UnitTests.Reflection.Extensions;

public class ParameterInfoExtensionsTests
{
     static ParameterInfo GetParameterInfo([Trim] string value) => MethodBase.GetCurrentMethod()!.GetParameters().First();
     static ParameterInfo GetParameterInfoWithoutAttributes(string value) => MethodBase.GetCurrentMethod()!.GetParameters().First();

     private ParameterInfo _testParameterInfo;
     
     public ParameterInfoExtensionsTests()
     {
          _testParameterInfo = GetParameterInfo(string.Empty);
     }
     
     [Fact]
     public void GetSanitizationAttribute_ShouldReturnTruthyResult_ForAttributeDeclaredOnParameter()
     {
          var res = _testParameterInfo.GetSanitizationAttribute<TrimAttribute>();
          res.Exists.Should().BeTrue();
          res.SanitizationAttribute.Should().BeOfType<TrimAttribute>().And.NotBeNull();
     }
     
     [Fact]
     public void GetSanitizationAttribute_ShouldReturnFalsyResult_ForAttributeNotDeclaredOnParameter()
     {
          var res = _testParameterInfo.GetSanitizationAttribute<EncodeHtmlAttribute>();
          res.Exists.Should().BeFalse();
          res.SanitizationAttribute.Should().BeNull();
     }
     
     [Fact]
     public void GetSanitizationAttribute_ShouldReturnFalsyResult_ForNoAttributeDeclared()
     {
          _testParameterInfo = GetParameterInfoWithoutAttributes(string.Empty);
          var res = _testParameterInfo.GetSanitizationAttribute<TrimAttribute>();
          res.Exists.Should().BeFalse();
          res.SanitizationAttribute.Should().BeNull();
     }
     
     [Fact]
     public void GetSanitizationMetaData_ShouldOnlyHaveTruthyResultForDeclaredParameterAttribute()
     {
          var res = _testParameterInfo.GetSanitizationMetaData();
          res.Should().BeEquivalentTo(
               new SanitizationMetaData(
                    trim: new ParameterSanitizationAttributeResult<TrimAttribute>(new TrimAttribute()),
                    encodeHtml: new ParameterSanitizationAttributeResult<EncodeHtmlAttribute>(null),
                    encodeJavaScript: new ParameterSanitizationAttributeResult<EncodeJavaScriptAttribute>(null),
                    encodeUrl: new ParameterSanitizationAttributeResult<EncodeURLAttribute>(null)
               )
          );
     }
     
     [Fact]
     public void GetSanitizationMetaData_ShouldOnlyHaveFalsyResultForNoDeclaredParameterAttribute()
     {
          _testParameterInfo = GetParameterInfoWithoutAttributes(string.Empty);
          var res = _testParameterInfo.GetSanitizationMetaData();
          res.Should().BeEquivalentTo(
               new SanitizationMetaData(
                    trim: new ParameterSanitizationAttributeResult<TrimAttribute>(null),
                    encodeHtml: new ParameterSanitizationAttributeResult<EncodeHtmlAttribute>(null),
                    encodeJavaScript: new ParameterSanitizationAttributeResult<EncodeJavaScriptAttribute>(null),
                    encodeUrl: new ParameterSanitizationAttributeResult<EncodeURLAttribute>(null)
               )
          );
     }
}