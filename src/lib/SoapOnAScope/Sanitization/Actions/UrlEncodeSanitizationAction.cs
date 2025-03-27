using System.Web;

namespace SoapOnAScope;

internal class UrlEncodeSanitizationAction(Func<string, bool> predicateToPerform) : ISanitizationAction
{
    public string? Perform(string? value) =>
        value is null || !predicateToPerform(value) ? value : HttpUtility.UrlEncode(value);
}
