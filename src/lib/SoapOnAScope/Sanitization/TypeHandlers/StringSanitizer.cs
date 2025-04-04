using SoapOnAScope.Reflection.Models;

namespace SoapOnAScope;

internal class StringSanitizer(SanitizationMetaData propertySanitizationMeta)
{
    private readonly ISanitizationAction[] _sanitizationActions =
    [
        new TrimSanitizationAction(_ => propertySanitizationMeta.Trim.Exists),
        new HtmlEncodeSanitizationAction(_ => propertySanitizationMeta.EncodeHtml.Exists),
        new UrlEncodeSanitizationAction(_ => propertySanitizationMeta.EncodeUrl.Exists),
        new JavascriptEncodeSanitizationAction(_ => propertySanitizationMeta.EncodeJavaScript.Exists),
    ];

    public string? PureSanitize(string? value)
    {
        if (value is null)
            return null;
        return _sanitizationActions.Aggregate(
            value,
            (current, sanitizationAction) => sanitizationAction.Perform(current)!
        );
    }
}
