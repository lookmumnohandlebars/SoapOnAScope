using System.Collections;
using SoapOnAScope.Reflection.Models;

namespace SoapOnAScope;

internal static class UnknownSanitizer
{
    public static bool Sanitize(object? input, SanitizationMetaData sanitizationMetaData)
    {
        if (input is null)
            return false;

        var type = input.GetType();
        
        if (input is string inputString)
        {
            input = new StringSanitizer(sanitizationMetaData).PureSanitize(inputString);
            return input is not null && inputString != input.ToString();
        }

        if (type.IsEnumerable())
        {
            if (input is IEnumerable)
                return EnumerableSanitizer.Sanitize(input, sanitizationMetaData);
        }

        if (type.IsClassLike())
        {
            return ClassSanitizer.Sanitize(input, sanitizationMetaData);
        }

        return false;
    }

    public static bool Sanitize(ref object? input,  SanitizationMetaData sanitizationMetaData)
    {
        if (input is null)
            return false;

        var type = input?.GetType();

        if (type?.IsEnumerable() ?? false)
        {
            if (input is IEnumerable inputEnumerable)
                return EnumerableSanitizer.Sanitize(inputEnumerable, sanitizationMetaData);
        }

        if (type?.IsClassLike() ?? false)
        {
            return ClassSanitizer.Sanitize(input, sanitizationMetaData);
        }

        return false;
    }
    
    public static object? PureSanitize(object? input,  SanitizationMetaData sanitizationMetaData)
    {
        if (input is null)
            return null;

        var output = null as object;

        if (input is string inputString)
        {
            output = new StringSanitizer(sanitizationMetaData).PureSanitize(inputString);
            return output;
        }

        var type = input?.GetType();
        output = input;

        if (type?.IsEnumerable() ?? false)
        {
            EnumerableSanitizer.Sanitize(output, sanitizationMetaData);
            return output;
        }

        if (type?.IsClassLike() ?? false)
        { 
            ClassSanitizer.Sanitize(output, sanitizationMetaData);
        }

        return output;
    }
}
