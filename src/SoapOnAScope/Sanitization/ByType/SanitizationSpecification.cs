namespace SoapOnAScope;

internal struct SanitizationSpecification(bool trim = true, bool htmlEncode = false)
{
    public bool Trim { get; } = trim;
    public bool HtmlEncode { get; } = htmlEncode;
}
