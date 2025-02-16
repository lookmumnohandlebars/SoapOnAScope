namespace SoapOnAScope;

internal class TrimSanitizationAction(Func<string, bool> predicateToPerform) : ISanitizationAction
{
    public string? Perform(string? value) =>
        value is null || !predicateToPerform(value) ? value : value.Trim();
}
