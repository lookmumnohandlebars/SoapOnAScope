using System;
using System.Linq;
using SoapOnAScope.Sanitization;

namespace SoapOnAScope;

internal class ClassSanitizer
{
    private readonly StringSanitizer _stringSanitizer;

    public ClassSanitizer(StringSanitizer stringSanitizer)
    {
        _stringSanitizer = stringSanitizer;
    }

    public bool Sanitize<T>(T model)
    {
        if (model is null)
            return false;
        var modelType = model.GetType();
        return SanitizePropertiesOnLevel(model, modelType);
    }

    private bool SanitizePropertiesOnLevel<T>(T model, Type modelType)
    {
        var hasSanitized = false;
        var properties = modelType.GetProperties();
        // TODO: Handle CanWrite being false with some backing field magic
        foreach (
            var stringProp in properties.Where(prop =>
                prop.PropertyType == typeof(string) && prop.CanWrite
            )
        )
        {
            hasSanitized = true;
            var propValue = (string?)stringProp.GetValue(model);
            if (propValue is null)
                continue;
            var sanitizedPropValue = _stringSanitizer.PureSanitize(propValue);
            stringProp.SetValue(model, sanitizedPropValue);
        }

        foreach (
            var nestedProp in properties.Where(prop =>
                prop.PropertyType.IsClass || prop.PropertyType.IsInterface
            )
        )
        {
            var nestedModel = nestedProp.GetValue(model);
            if (nestedModel is null)
                continue;
            hasSanitized =
                SanitizePropertiesOnLevel(nestedProp.GetValue(model), nestedProp.PropertyType)
                || hasSanitized;
        }

        return hasSanitized;
    }
}
