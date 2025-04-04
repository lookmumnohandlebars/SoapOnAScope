using System.Collections;
using SoapOnAScope.Reflection.Models;

namespace SoapOnAScope;

internal static class EnumerableSanitizer
{
    public static bool Sanitize(object? enumerableObject, SanitizationMetaData sanitizationMetaData)
    {
        if (enumerableObject is null)
            return false;
        
        var type = enumerableObject.GetType();

        if (!type.IsEnumerable()) return false;
        
        var hasSanitized = HandleArray(enumerableObject, type, sanitizationMetaData);
        hasSanitized = hasSanitized || HandleList(enumerableObject, type, sanitizationMetaData);
        hasSanitized = hasSanitized || HandleDictionary(enumerableObject, type, sanitizationMetaData);

        return hasSanitized;
    }

    private static bool HandleArray(object enumerableObject, Type type, SanitizationMetaData sanitizationMetaData)
    {
        var hasSanitized = false;
        if (!type.IsArray) return hasSanitized;
        var array = (object?[])enumerableObject;
        for (var i = 0; i < array.Length; i++)
        {
            var elementValue = array[i];
            UnknownSanitizer.Sanitize(elementValue, sanitizationMetaData);
            array[i] = elementValue;
            hasSanitized = hasSanitized || elementValue != array[i];
        }
        return hasSanitized;
    }
    private static bool HandleList(object enumerableObject, Type type, SanitizationMetaData sanitizationMetaData)
    {
        var hasSanitized = false;
        if (!type.IsList()) return hasSanitized;
        var list = (IList)enumerableObject;
        for (var i = 0; i < list.Count; i++)
        {
            var elementValue = list[i];
            UnknownSanitizer.Sanitize(elementValue, sanitizationMetaData);
            list[i] = elementValue;
            hasSanitized = hasSanitized || elementValue != list[i];
        }
        return hasSanitized;
    }
    private static bool HandleDictionary(object enumerableObject, Type type, SanitizationMetaData sanitizationMetaData)
    {
        var hasSanitized = false;
        if (!type.IsDictionary()) return hasSanitized;
        var dictionary = (IDictionary)enumerableObject;
        foreach (var key in dictionary.Keys)
        {
            var elementValue = dictionary[key];
            dictionary.Remove(key);
            var hasKeySanitized = UnknownSanitizer.Sanitize(key, sanitizationMetaData);
            var hasValueSanitized = UnknownSanitizer.Sanitize(elementValue, sanitizationMetaData);
            dictionary[key] = elementValue;
            hasSanitized = hasSanitized || hasKeySanitized || hasValueSanitized;
        }
        return hasSanitized;
    }
}
