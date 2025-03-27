using System;

namespace SoapOnAScope;

/// <summary>
///     An attribute that indicates that a class or struct should be sanitized automatically (and recursively).
///     This attribute is used by the <see cref="Sanitizer"/> class to automatically sanitize
///     the properties of a class or struct.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class AutoSanitizeAttribute : Attribute
{
    internal bool Trim { get; }

    internal bool Url { get; }

    internal bool Html { get; }

    internal bool JavaScript { get; }

    /// <inheritdoc cref="AutoSanitizeAttribute"/>
    /// <param name="trim"></param>
    /// <param name="url"></param>
    /// <param name="html"></param>
    /// <param name="js"></param>
    public AutoSanitizeAttribute(bool trim = true, bool url = false, bool html = false, bool js = false)
    {
        Trim = trim;
        Url = url;
        Html = html;
        JavaScript = js;
    }
}
