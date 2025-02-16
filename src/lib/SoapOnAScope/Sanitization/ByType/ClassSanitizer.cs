using System.Collections;
using System.Reflection;
using SoapOnAScope.Sanitization;

namespace SoapOnAScope;

internal static class ClassSanitizer
{
    public static bool Sanitize<T>(
        T? model,
        SanitizationSpecification? parentSanitizationAttribute = null
    )
        where T : class
    {
        if (model is null)
            return false;
        var modelType = model.GetType();
        var specResult = parentSanitizationAttribute is not null
            ? new SanitizationAttributeResult(
                true,
                (SanitizationSpecification)parentSanitizationAttribute
            )
            : new SanitizationAttributeResult(false, new SanitizationSpecification());
        if (modelType.IsEnumerable())
            EnumerableSanitizer.Sanitize((IEnumerable)model, specResult.Specification);
        return SanitizeClassProperties(model, modelType, specResult);
    }

    private static bool SanitizeClassProperties<T>(
        T model,
        Type modelType,
        SanitizationAttributeResult? parentSanitizationAttribute = null
    )
    {
        var properties = modelType.GetProperties();
        var autoSanitizationAttribute = modelType.GetSpecFromAttribute();
        var hasPropertySanitized = HandleStringProperties(
            model,
            autoSanitizationAttribute,
            properties
        );
        var hasObjectSanitized = HandleObjectProperties(
            model,
            autoSanitizationAttribute,
            properties
        );
        var hasEnumerableSanitized = HandleEnumerableProperties(
            model,
            autoSanitizationAttribute,
            properties
        );
        return hasPropertySanitized || hasObjectSanitized || hasEnumerableSanitized;
    }

    private static bool HandleStringProperties<T>(
        T model,
        SanitizationAttributeResult autoSanitizationAttribute,
        IEnumerable<PropertyInfo> properties
    ) =>
        properties
            .WhereWriteableString()
            .Aggregate(
                false,
                (current, stringProp) =>
                    StringPropertySanitizer.Sanitize(stringProp, model, autoSanitizationAttribute)
                    || current
            );

    private static bool HandleObjectProperties<T>(
        T model,
        SanitizationAttributeResult autoSanitizationAttribute,
        IEnumerable<PropertyInfo> properties
    )
    {
        var hasSanitized = false;
        foreach (var nestedProp in properties.WhereObjectProperty())
        {
            var nestedModel = nestedProp.GetValue(model);
            if (nestedModel is null)
                continue;
            hasSanitized =
                SanitizeClassProperties(
                    nestedModel,
                    nestedProp.PropertyType,
                    autoSanitizationAttribute
                ) || hasSanitized;
        }

        return hasSanitized;
    }

    private static bool HandleEnumerableProperties<T>(
        T model,
        SanitizationAttributeResult autoSanitizationAttribute,
        IEnumerable<PropertyInfo> properties
    )
    {
        var hasSanitized = false;
        foreach (var nestedProp in properties.WhereEnumerableProperty())
        {
            var nestedModel = nestedProp.GetValue(model);
            if (nestedModel is null)
                continue;
            hasSanitized =
                EnumerableSanitizer.Sanitize(
                    (IEnumerable)nestedModel,
                    autoSanitizationAttribute.Specification
                ) || hasSanitized;
        }

        return hasSanitized;
    }
}
