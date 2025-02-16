namespace SoapOnAScope.UnitTests.TestModels;

public class EnumerablesClass
{
    [Sanitize(trim: true)]
    public IEnumerable<string> StringsToTrim { get; set; } = new List<string>() { " TRIM ME " };
}
