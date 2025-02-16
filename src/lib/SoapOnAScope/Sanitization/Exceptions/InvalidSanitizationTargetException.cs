namespace SoapOnAScope;

/// <summary>
///     Signals a property that is not valid Sanitization
/// </summary>
public class InvalidSanitizationTargetException : InvalidOperationException
{
    /// <inheritdoc cref="InvalidSanitizationTargetException"/>
    /// <param name="property">The name of the invalid target property</param>
    /// <param name="reason">The reason the target property is invalid</param>
    public InvalidSanitizationTargetException(string property, string reason)
        : base($"{property} is an invalid target for sanitization; {reason}") { }
}
