using SoapOnAScope.Reflection.Models;

namespace SoapOnAScope;

/// <summary>
///     Single static utility facade for performing inline sanitization
///     Similar in design to the DataAnnotations `Validator` class
/// </summary>
public static class Sanitizer
{
    /// <summary>
    ///     Sanitize string properties belonging to an object, or class instance,
    ///     
    /// </summary>
    /// <param name="instance">Instance to be sanitized</param>
    /// <typeparam name="T">The type of the instance</typeparam>
    /// <example>
    ///     <code>
    ///         Sanitizer.Sanitize(new MyModel());
    ///     </code>
    /// </example>
    public static void Sanitize<T>(T? instance)
        where T : class
    {
        ClassSanitizer.Sanitize(instance);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="sanitizationMetaData"></param>
    public static void SanitizeObject(object? instance, SanitizationMetaData sanitizationMetaData)
    {
        UnknownSanitizer.Sanitize(instance, sanitizationMetaData);
    }

    /// <summary>
    ///     Sanitize a string variable (by reference) inline
    /// </summary>
    /// <param name="stringValue"></param>
    /// <param name="sanitizationMetaData"></param>
    public static void SanitizeString(ref string? stringValue, SanitizationMetaData sanitizationMetaData)
    {
        if (stringValue is null) return;
        var stringSanitizer = new StringSanitizer(sanitizationMetaData);
        stringValue = stringSanitizer.PureSanitize(stringValue);
    }
}
