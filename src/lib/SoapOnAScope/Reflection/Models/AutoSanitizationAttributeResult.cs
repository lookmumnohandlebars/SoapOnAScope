namespace SoapOnAScope.Reflection.Models;

internal class AutoSanitizationAttributeResult<TAttribute, TUnderlying>(TAttribute? attribute)
    where TAttribute : BaseAutoSanitizationAttribute<TUnderlying>
    where TUnderlying : BaseSanitizationAttribute
{
    public TAttribute? SanitizationAttribute { get; } = attribute;
    public bool Exists => SanitizationAttribute is not null;
        
    public static implicit operator TAttribute?(AutoSanitizationAttributeResult<TAttribute, TUnderlying> result) => result.SanitizationAttribute;
    public static implicit operator AutoSanitizationAttributeResult<TAttribute, TUnderlying>(TAttribute? result) => new(result);
    
    public PropertySanitizationAttributeResult<TUnderlying> ToUnderlyingResultAttribute() => SanitizationAttribute?.UnderlyingAttribute;
}