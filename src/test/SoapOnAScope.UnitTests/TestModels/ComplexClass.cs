namespace SoapOnAScope.UnitTests.TestModels;

public class ComplexClass
{
    #region Non-String Primitives

    public int Id { get; set; } = 41;
    public int InternalId { get; } = 40;
    private long PrivateId { get; } = 42;
    private short? NullableId { get; } = null;

    #endregion

    #region Strings
    public string SettableString { get; set; } = nameof(SettableString);

    public string EmptyString { get; set; } = string.Empty;
    public string TrimString { get; set; } = StringTestCases.Trim.NeedsTrimming;

    #endregion

    #region Nested Classes



    #endregion
}
