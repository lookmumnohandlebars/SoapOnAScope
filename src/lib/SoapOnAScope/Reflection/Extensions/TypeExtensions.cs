using System.Collections;
using System.Reflection;
using SoapOnAScope.Reflection.Models;

namespace SoapOnAScope;

internal static class TypeExtensions
{
    public static AutoSanitizationAttributeResult<TAuto,TUnderlying> GetAutoSanitizationAttribute<TAuto, TUnderlying>(this Type type)
        where TAuto : BaseAutoSanitizationAttribute<TUnderlying>
        where TUnderlying : BaseSanitizationAttribute
        => new(type.GetCustomAttribute<TAuto>());
    
    public static AutoSanitizationMetaData GetAutoSanitizationMetaData(this Type type)
    {
        if (!type.IsClassLike()) throw new InvalidOperationException("Internal Error: Auto Sanitization can only be applied to classes or interfaces");
        return new AutoSanitizationMetaData(
            trim: type.GetAutoSanitizationAttribute<AutoTrimAttribute, TrimAttribute>()
        );
    }

    public static bool IsClassLike(this Type type) => type.IsClass || type.IsInterface;

    public static bool IsEnumerable(this Type type) => type.IsAssignableFrom(typeof(IEnumerable<>));
    public static bool IsList(this Type type) => type.IsAssignableFrom(typeof(IList));
    public static bool IsDictionary(this Type type) => type.IsAssignableFrom(typeof(IDictionary));

}
