using System.Collections;
using System.Reflection;

namespace SoapOnAScope;

internal static class PropertyInfoExtensions
{
    public static IEnumerable<PropertyInfo> WhereWriteableString(
        this IEnumerable<PropertyInfo> properties
    ) => properties.Where(prop => prop.IsWriteableString());

    public static bool IsWriteableString(this PropertyInfo prop) =>
        prop.PropertyType == typeof(string) && prop.CanWrite;

    public static IEnumerable<PropertyInfo> WhereEnumerableProperty(
        this IEnumerable<PropertyInfo> properties
    )
    {
        var enumerableType = typeof(IEnumerable);
        return properties.Where(prop =>
            prop.PropertyType != typeof(string)
            && enumerableType.IsAssignableFrom(prop.PropertyType)
        );
    }

    public static IEnumerable<PropertyInfo> WhereObjectProperty(
        this IEnumerable<PropertyInfo> properties
    ) => properties.Where(prop => prop.PropertyType.IsClass || prop.PropertyType.IsInterface);

    public static SanitizationAttributeResult GetPropertySanitizationAttribute(
        this PropertyInfo propertyInfo
    )
    {
        //This may need to be caught, null handling may not be enough
        var attr = propertyInfo.GetCustomAttribute<SanitizeAttribute>();
        if (attr is null)
            return new SanitizationAttributeResult(false, new SanitizationSpecification());
        return new SanitizationAttributeResult(
            true,
            new SanitizationSpecification(
                Trim: attr.Trim,
                HtmlEncode: attr.Html,
                UrlEncode: attr.Url,
                JsEncode: attr.JavaScript
            )
        );
    }
}
