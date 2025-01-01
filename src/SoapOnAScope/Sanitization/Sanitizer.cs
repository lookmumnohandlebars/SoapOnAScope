using SoapOnAScope.Sanitization.Object;

namespace SoapOnAScope.Sanitization;

/// <summary>
///     Single utility facade for performing inline sanitization
///
///     Similar in design to the DataAnnotations `Validator` class
/// </summary>
public static class Sanitizer
{
    public static void Sanitize<T>(T instance)
        where T : class
    {
        var classSanitizer = new ClassSanitizer(
            new StringSanitizer(new SanitizationSpecification())
        );
        classSanitizer.Sanitize(instance);
    }
}
