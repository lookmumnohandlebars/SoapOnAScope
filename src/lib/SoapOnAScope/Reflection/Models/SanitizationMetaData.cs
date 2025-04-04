namespace SoapOnAScope.Reflection.Models;

/// <summary>
/// 
/// </summary>
public readonly struct SanitizationMetaData : IEquatable<SanitizationMetaData>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="trim"></param>
    /// <param name="encodeHtml"></param>
    /// <param name="encodeJavaScript"></param>
    /// <param name="encodeUrl"></param>
    internal SanitizationMetaData(SanitizationAttributeResult<TrimAttribute> trim,
        SanitizationAttributeResult<EncodeHtmlAttribute> encodeHtml,
        SanitizationAttributeResult<EncodeJavaScriptAttribute> encodeJavaScript,
        SanitizationAttributeResult<EncodeURLAttribute> encodeUrl)
    {
        Trim = trim;
        EncodeHtml = encodeHtml;
        EncodeJavaScript = encodeJavaScript;
        EncodeUrl = encodeUrl;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="trim"></param>
    /// <param name="encodeHtml"></param>
    /// <param name="encodeJavaScript"></param>
    /// <param name="encodeUrl"></param>
    public SanitizationMetaData(
        bool trim = false, 
        bool encodeHtml = false, 
        bool encodeJavaScript = false, 
        bool encodeUrl = false
    ) 
        : this(trim ? new SanitizationAttributeResult<TrimAttribute>(new TrimAttribute()) : new SanitizationAttributeResult<TrimAttribute>(null),
            encodeHtml ? new SanitizationAttributeResult<EncodeHtmlAttribute>(new EncodeHtmlAttribute()) : new SanitizationAttributeResult<EncodeHtmlAttribute>(null),
            encodeJavaScript ? new SanitizationAttributeResult<EncodeJavaScriptAttribute>(new EncodeJavaScriptAttribute()) : new SanitizationAttributeResult<EncodeJavaScriptAttribute>(null),
            encodeUrl ? new SanitizationAttributeResult<EncodeURLAttribute>(new EncodeURLAttribute()) : new SanitizationAttributeResult<EncodeURLAttribute>(null))
    { }

    internal SanitizationAttributeResult<TrimAttribute> Trim { get; }
    internal SanitizationAttributeResult<EncodeHtmlAttribute> EncodeHtml { get; }
    internal SanitizationAttributeResult<EncodeJavaScriptAttribute> EncodeJavaScript { get; }
    internal SanitizationAttributeResult<EncodeURLAttribute> EncodeUrl { get;}
    
    internal SanitizationMetaData Merge(SanitizationMetaData other)
    {
        return new SanitizationMetaData(
            MergeResult(Trim, other.Trim), 
            MergeResult(EncodeHtml, other.EncodeHtml),
            MergeResult(EncodeJavaScript, other.EncodeJavaScript),
            MergeResult(EncodeUrl, other.EncodeUrl)
        );
            
        SanitizationAttributeResult<TAttribute> MergeResult<TAttribute>(SanitizationAttributeResult<TAttribute> initial, SanitizationAttributeResult<TAttribute> other)
            where TAttribute : BaseSanitizationAttribute =>
            initial.Exists ? initial : other.Exists ? other : initial;
    }

    /// <inheritdoc />
    public bool Equals(SanitizationMetaData other) => Trim.Equals(other.Trim) && EncodeHtml.Equals(other.EncodeHtml) && EncodeJavaScript.Equals(other.EncodeJavaScript) && EncodeUrl.Equals(other.EncodeUrl);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is SanitizationMetaData other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Trim, EncodeHtml, EncodeJavaScript, EncodeUrl);
}