using System;

namespace SoapOnAScope;

/// <summary>
///
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class SanitizeAttribute : Attribute
{
    internal bool Trim { get; }

    internal bool Url { get; }

    internal bool Html { get; }

    internal bool JavaScript { get; }

    /// <inheritdoc/>
    /// <param name="trim"> Defaults to `true`</param>
    /// <param name="html"> Defaults to `false`</param>
    /// <param name="url"> Defaults to `false`</param>
    /// <param name="js"> Defaults to `false`</param>
    public SanitizeAttribute(bool trim = true, bool html = false, bool url = false, bool js = false)
    { }
}
