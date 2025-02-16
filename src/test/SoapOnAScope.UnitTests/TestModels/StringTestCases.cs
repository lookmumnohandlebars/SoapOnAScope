namespace SoapOnAScope.UnitTests.TestModels;

public static class StringTestCases
{
    #region Empty Strings
    public static class Empty
    {
        public const string Null = null!;
        public const string Blank = "";
        public const string Spaces = "         ";
        public const string NewLine = "\n";
    }
    #endregion

    #region TrimCases
    public static class Trim
    {
        public const string BaseString = "Trim";
        public const string AlreadyTrimmed = $"{BaseString}";
        public const string NeedsTrimming = $"     {BaseString}     ";
    }
    #endregion

    #region HtmlEncodeCases
    public static class Html
    {
        public const string BaseString = "<div>EncodeMe!</div>";
        public const string EncodedString = "&lt;div&gt;EncodeMe!&lt;/div&gt;";
    }
    #endregion

    #region UrlEncodeCases
    public static class Url
    {
        public const string BaseString =
            "https://www.google.com/search?q=SoapOnAScope&rlz=1C1GCEU_enUS832US832&oq=SoapOnAScope&aqs=chrome69i57j0i512l9.1337j0j7&sourceid=chrome&ie=UTF-8";
        public const string AlreadyEncoded =
            "https%3a%2f%2fwww.google.com%2fsearch%3fq%3dSoapOnAScope%26rlz%3d1C1GCEU_enUS832US832%26oq%3dSoapOnAScope%26aqs%3dchrome69i57j0i512l9.1337j0j7%26sourceid%3dchrome%26ie%3dUTF-8";
    }
    #endregion

    #region JsEncodeCases
    public static class Js
    {
        public const string BaseString = "<script>alert('XSS')</script>";
        public const string EncodedString = "&lt;script&gt;alert(&#39;XSS&#39;)&lt;/script&gt;";
    }
    #endregion
}
