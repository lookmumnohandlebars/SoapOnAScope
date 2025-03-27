using System.Collections;

namespace SoapOnAScope;

internal static class UnknownSanitizer
{
    public static bool Sanitize(object? input, SanitizationSpecification sanitizationSpecification)
    {
        if (input is null)
            return false;

        var type = input.GetType();

        if (type.IsEnumerable())
        {
            if (input is IEnumerable inputEnumerable)
                return EnumerableSanitizer.Sanitize(inputEnumerable, sanitizationSpecification);
        }

        if (type.IsClassLike())
        {
            return ClassSanitizer.Sanitize(input, sanitizationSpecification);
        }

        return false;
    }

    public static bool Sanitize(ref object? input, SanitizationSpecification sanitizationSpecification)
    {
        if (input is null)
            return false;

        if (input is string inputString)
        {
            input = new StringSanitizer(sanitizationSpecification).PureSanitize(inputString);
        }

        var type = input?.GetType();

        if (type?.IsEnumerable() ?? false)
        {
            if (input is IEnumerable inputEnumerable)
                return EnumerableSanitizer.Sanitize(inputEnumerable, sanitizationSpecification);
        }

        if (type?.IsClassLike() ?? false)
        {
            return ClassSanitizer.Sanitize(input, sanitizationSpecification);
        }

        return false;
    }
}
