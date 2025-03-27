using System.Web;

namespace SoapOnAScope;

internal class JavascriptEncodeSanitizationAction(Func<string, bool> predicateToPerform) : ISanitizationAction
{
    public string? Perform(string? value) =>
        value is null || !predicateToPerform(value) ? value : HttpUtility.JavaScriptStringEncode(value);
}
