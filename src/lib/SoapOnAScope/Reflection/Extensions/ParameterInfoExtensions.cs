using System.Reflection;
using SoapOnAScope.Reflection.Models;

namespace SoapOnAScope;

internal static class ParameterInfoExtensions
{
    public static ParameterSanitizationAttributeResult<T> GetSanitizationAttribute<T>(this ParameterInfo parameterInfo)
        where T : BaseSanitizationAttribute => new (parameterInfo.GetCustomAttribute<T>());
    
    public static SanitizationMetaData GetSanitizationMetaData(this ParameterInfo propertyInfo)
    {
        //This may need to be caught, null handling may not be enough
        return new SanitizationMetaData(
            trim: GetSanitizationAttribute<TrimAttribute>(propertyInfo),
            encodeHtml: GetSanitizationAttribute<EncodeHtmlAttribute>(propertyInfo),
            encodeJavaScript: GetSanitizationAttribute<EncodeJavaScriptAttribute>(propertyInfo),
            encodeUrl: GetSanitizationAttribute<EncodeURLAttribute>(propertyInfo)
        );
    }
}