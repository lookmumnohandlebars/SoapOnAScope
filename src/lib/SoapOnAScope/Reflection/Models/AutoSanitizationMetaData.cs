namespace SoapOnAScope.Reflection.Models;

internal readonly struct AutoSanitizationMetaData(
    AutoSanitizationAttributeResult<AutoTrimAttribute, TrimAttribute> trim)
{
    public AutoSanitizationAttributeResult<AutoTrimAttribute, TrimAttribute> Trim { get; } = trim;

    public SanitizationMetaData ToSanitizationMetaData()
    {
        return new SanitizationMetaData(
            trim: Trim.ToUnderlyingResultAttribute(),
            new SanitizationAttributeResult<EncodeHtmlAttribute>(null),
            new SanitizationAttributeResult<EncodeJavaScriptAttribute>(null),
            new SanitizationAttributeResult<EncodeURLAttribute>(null)
        );
    }
}