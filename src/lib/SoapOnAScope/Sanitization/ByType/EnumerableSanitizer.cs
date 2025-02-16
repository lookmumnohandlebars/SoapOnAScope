using System.Collections;

namespace SoapOnAScope;

internal static class EnumerableSanitizer
{
    public static bool Sanitize(
        IEnumerable enumerable,
        SanitizationSpecification sanitizationSpecification
    )
    {
        var res = false;

        if (enumerable is List<string> strList)
        {
            for (var i = 0; i < strList.Count; i++)
            {
                strList[i] =
                    new StringSanitizer(sanitizationSpecification).PureSanitize(strList[i])
                    ?? string.Empty;
            }

            return true;
        }

        if (enumerable is string[] strArray)
        {
            for (var i = 0; i < strArray.Length; i++)
            {
                strArray[i] =
                    new StringSanitizer(sanitizationSpecification).PureSanitize(strArray[i])
                    ?? string.Empty;
            }

            return true;
        }

        foreach (var obj in enumerable)
        {
            res = UnknownSanitizer.Sanitize(obj, sanitizationSpecification) || res;
        }

        return res;
    }
}
