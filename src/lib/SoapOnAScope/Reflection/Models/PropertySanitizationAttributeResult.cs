namespace SoapOnAScope.Reflection.Models;

internal class PropertySanitizationAttributeResult<TAttribute> : SanitizationAttributeResult<TAttribute> 
    where TAttribute : BaseSanitizationAttribute
{
    public PropertySanitizationAttributeResult(TAttribute? attribute) : base(attribute)
    { }
    
    public static implicit operator TAttribute?(PropertySanitizationAttributeResult<TAttribute> result) => result.SanitizationAttribute;
    public static implicit operator PropertySanitizationAttributeResult<TAttribute>(TAttribute? result) => new(result);
    
    public static PropertySanitizationAttributeResult<TAttribute> Empty() =>
        new(null);
}