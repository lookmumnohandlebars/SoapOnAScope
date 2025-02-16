namespace SoapOnAScope;

/// <summary>
///     Represents a set of sanitization actions to be performed on a string
/// </summary>
/// <param name="Trim">Enables trimming on sanitization. </param>
/// <param name="HtmlEncode"></param>
/// <param name="UrlEncode"></param>
/// <param name="JsEncode"></param>
public record struct SanitizationSpecification(
    bool Trim = false,
    bool HtmlEncode = false,
    bool UrlEncode = false,
    bool JsEncode = false
);
