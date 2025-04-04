namespace SoapOnAScope.UnitTests.TestModels;

public class EnumerablesClass
{
    public IEnumerable<string> StringsToTrim { get; set; } = new List<string>() { " TRIM ME " };
}
