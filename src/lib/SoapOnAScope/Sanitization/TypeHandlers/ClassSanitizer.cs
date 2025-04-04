using System.Collections;
using System.Reflection;
using SoapOnAScope.Reflection.Models;
using SoapOnAScope.Sanitization;

namespace SoapOnAScope;

internal static class ClassSanitizer
{
    /// <summary>
    ///     Sanitizes the instance of a class given the declared attributes
    /// </summary>
    /// <param name="model">The instance of the model to be sanitized</param>
    /// <param name="parentMetaData">
    ///     If null is passed in, we assume that we are at the top level class.
    /// </param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool Sanitize<T>(T? model, SanitizationMetaData? parentMetaData = null)
        where T : class
    {
        if (model is null)
            return false;
        var modelType = model.GetType();

        var autoSanitizationMetaData = modelType.GetAutoSanitizationMetaData().ToSanitizationMetaData();
        var mergedSanitizationMetaData = IsTopLevelClass(parentMetaData) ? autoSanitizationMetaData : autoSanitizationMetaData.Merge(parentMetaData!.Value);
        
        if (modelType.IsEnumerable())
            EnumerableSanitizer.Sanitize((ICollection)model, mergedSanitizationMetaData);
        return SanitizeClassProperties(model, modelType, mergedSanitizationMetaData);
    }

    private static bool IsTopLevelClass(SanitizationMetaData? parentSanitizationAttribute = null) =>
        parentSanitizationAttribute is null;

    private static bool SanitizeClassProperties<T>(
        T model,
        Type modelType,
        SanitizationMetaData parentMetaData
    )
    {
        var properties = modelType.GetProperties();
        var hasPropertySanitized = HandleStringProperties(model, parentMetaData, properties);
        var hasObjectSanitized = HandleObjectProperties(model, parentMetaData, properties);
        var hasEnumerableSanitized = HandleEnumerableProperties(model, parentMetaData, properties);
        return hasPropertySanitized || hasObjectSanitized || hasEnumerableSanitized;
    }

    private static bool HandleStringProperties<T>(
        T model,
        SanitizationMetaData sanitizationMetaData,
        IEnumerable<PropertyInfo> properties
    ) =>
        properties
            .WhereWriteableString()
            .Aggregate(
                false,
                (current, stringProp) =>
                    StringPropertySanitizer.Sanitize(stringProp, model, sanitizationMetaData) || current
            );

    private static bool HandleObjectProperties<T>(
        T model,
        SanitizationMetaData sanitizationMetaData,
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
                SanitizeClassProperties(nestedModel, nestedProp.PropertyType, sanitizationMetaData)
                || hasSanitized;
        }

        return hasSanitized;
    }

    private static bool HandleEnumerableProperties<T>(
        T model,
        SanitizationMetaData sanitizationMetaData,
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
                EnumerableSanitizer.Sanitize((ICollection)nestedModel, sanitizationMetaData)
                || hasSanitized;
        }

        return hasSanitized;
    }
}
