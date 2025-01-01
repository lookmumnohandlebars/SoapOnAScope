using System.Web;

namespace SoapOnAScope.Sanitization;

internal class StringSanitizer
{
    private readonly SanitizationSpecification _spec;

    public StringSanitizer(SanitizationSpecification spec)
    {
        _spec = spec;
    }

    public string PureSanitize(string value)
    {
        var output = string.Copy(value);
        if (_spec.Trim)
        {
            output = output.Trim();
        }

        if (_spec.HtmlEncode)
        {
            output = HttpUtility.HtmlEncode(output);
        }

        return output;
    }
}
