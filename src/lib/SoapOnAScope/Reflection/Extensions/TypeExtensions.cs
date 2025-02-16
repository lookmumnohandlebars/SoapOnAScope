using System.Reflection;

namespace SoapOnAScope;

internal static class TypeExtensions
{
    public static SanitizationAttributeResult GetSpecFromAttribute(this Type type)
    {
        //This may need to be caught, null handling may not be enough
        var attr = type.GetCustomAttribute<AutoSanitizeAttribute>();
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

    public static bool IsClassLike(this Type type) => type.IsClass || type.IsInterface;

    public static bool IsEnumerable(this Type type) => type.IsAssignableFrom(typeof(IEnumerable<>));
}
