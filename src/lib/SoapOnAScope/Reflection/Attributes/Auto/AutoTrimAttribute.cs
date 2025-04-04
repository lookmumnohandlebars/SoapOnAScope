namespace SoapOnAScope;

/// <summary>
///     
/// </summary>
/// <example>
/// 
/// </example>
public class AutoTrimAttribute : BaseAutoSanitizationAttribute<TrimAttribute>
{
    internal override TrimAttribute UnderlyingAttribute { get; } = new();
}