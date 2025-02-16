namespace SoapOnAScope;

internal interface ISanitizationAction
{
    string? Perform(string? value);
}
