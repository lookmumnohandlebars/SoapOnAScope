using System.Collections;
using System.Reflection;
using SoapOnAScope.Reflection.Models;

namespace SoapOnAScope;

internal static class PropertyInfoExtensions
{
    public static IEnumerable<PropertyInfo> WhereWriteableString(this IEnumerable<PropertyInfo> properties) =>
        properties.Where(prop => prop.IsWriteableString());

    public static bool IsWriteableString(this PropertyInfo prop) =>
        prop.PropertyType == typeof(string) && prop.CanWrite;

    public static IEnumerable<PropertyInfo> WhereEnumerableProperty(this IEnumerable<PropertyInfo> properties)
    {
        var enumerableType = typeof(IEnumerable);
        return properties.Where(prop =>
            prop.PropertyType != typeof(string) && enumerableType.IsAssignableFrom(prop.PropertyType)
        );
    }

    public static IEnumerable<PropertyInfo> WhereObjectProperty(this IEnumerable<PropertyInfo> properties) =>
        properties.Where(prop => prop.PropertyType.IsClass || prop.PropertyType.IsInterface);

    public static PropertySanitizationAttributeResult<T> GetSanitizationAttribute<T>(this PropertyInfo propertyInfo)
        where T : BaseSanitizationAttribute => new (propertyInfo.GetCustomAttribute<T>());

    public static SanitizationMetaData GetPropertySanitizationMetaData(this PropertyInfo propertyInfo) =>
        new(
            trim: GetSanitizationAttribute<TrimAttribute>(propertyInfo),
            encodeHtml: GetSanitizationAttribute<EncodeHtmlAttribute>(propertyInfo),
            encodeJavaScript: GetSanitizationAttribute<EncodeJavaScriptAttribute>(propertyInfo),
            encodeUrl: GetSanitizationAttribute<EncodeURLAttribute>(propertyInfo)
        );
}
