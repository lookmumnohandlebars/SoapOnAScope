namespace SoapOnAScope;

internal class StringSanitizer(SanitizationSpecification spec)
{
    private readonly ISanitizationAction[] _sanitizationActions =
    [
        new TrimSanitizationAction(_ => spec.Trim),
        new HtmlEncodeSanitizationAction(_ => spec.HtmlEncode),
        new UrlEncodeSanitizationAction(_ => spec.UrlEncode),
        new JavascriptEncodeSanitizationAction(_ => spec.JsEncode),
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
