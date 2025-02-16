namespace SoapOnAScope;

/// <summary>
///     Single static utility facade for performing inline sanitization
///     Similar in design to the DataAnnotations `Validator` class
/// </summary>
public static class Sanitizer
{
    /// <summary>
    ///     Sanitize string properties belonging to an object, or class instance,
    ///     that either has the <see cref="AutoSanitizeAttribute"/> applied at the top level
    ///     or has the <see cref="SanitizeAttribute"/> applied to a property
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
    ///     Sanitize a string variable (by reference) inline
    /// </summary>
    /// <param name="stringValue"></param>
    /// <param name="specification"></param>
    public static void Sanitize(
        ref string? stringValue,
        SanitizationSpecification? specification = null
    )
    {
        var specs = specification ?? new SanitizationSpecification(Trim: true);
        var stringSanitizer = new StringSanitizer(specs);
        stringValue = stringSanitizer.PureSanitize(stringValue);
    }
}
