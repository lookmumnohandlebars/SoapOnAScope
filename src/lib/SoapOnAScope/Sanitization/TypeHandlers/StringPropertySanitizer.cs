using System.Reflection;
using SoapOnAScope.Reflection.Models;

namespace SoapOnAScope.Sanitization;

internal static class StringPropertySanitizer
{
    public static Action<string, string, string?>? LoggingAction = null;

    public static bool Sanitize<T>(
        PropertyInfo stringPropertyInfo,
        T parentModel,
        SanitizationMetaData parentMetaData
    )
    {
        if (!stringPropertyInfo.IsWriteableString())
            return false;

        var propValue = (string?)stringPropertyInfo.GetValue(parentModel);
        if (propValue is null)
            return false;

        var combinedSanitizationMetaData = stringPropertyInfo
            .GetPropertySanitizationMetaData()
            .Merge(parentMetaData);
        
        return SanitizeString(propValue, stringPropertyInfo, parentModel, combinedSanitizationMetaData);
    }

    private static bool SanitizeString<T>(
        string propValue,
        PropertyInfo propInfo,
        T parentModel,
        SanitizationMetaData spec
    )
    {
        var stringSanitizer = new StringSanitizer(spec);
        var sanitizedPropValue = stringSanitizer.PureSanitize(propValue);
        propInfo.SetValue(parentModel, sanitizedPropValue);
        LoggingAction?.Invoke(propInfo.Name, propValue, sanitizedPropValue);
        return propValue != sanitizedPropValue;
    }
}
