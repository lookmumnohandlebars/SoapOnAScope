using System.Reflection;

namespace SoapOnAScope.Sanitization;

internal static class StringPropertySanitizer
{
    public static Action<string, string, string?>? LoggingAction = null;

    public static bool Sanitize<T>(
        PropertyInfo stringPropertyInfo,
        T parentModel,
        SanitizationAttributeResult parentAttribute
    )
    {
        if (!stringPropertyInfo.IsWriteableString())
            return false;

        var propValue = (string?)stringPropertyInfo.GetValue(parentModel);
        if (propValue is null)
            return false;

        var (isPropEnabled, specFromProp) = stringPropertyInfo.GetPropertySanitizationAttribute();
        if (!isPropEnabled && !parentAttribute.IsEnabled)
            return false;

        var specToUse = isPropEnabled ? specFromProp : parentAttribute.Specification;
        SanitizeString(propValue, stringPropertyInfo, parentModel, specToUse);
        return true;
    }

    private static void SanitizeString<T>(
        string propValue,
        PropertyInfo propInfo,
        T parentModel,
        SanitizationSpecification spec
    )
    {
        var stringSanitizer = new StringSanitizer(spec);
        var sanitizedPropValue = stringSanitizer.PureSanitize(propValue);
        propInfo.SetValue(parentModel, sanitizedPropValue);
        LoggingAction?.Invoke(propInfo.Name, propValue, sanitizedPropValue);
    }
}
