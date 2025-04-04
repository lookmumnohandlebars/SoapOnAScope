namespace SoapOnAScope.Reflection.Models;

internal class ParameterSanitizationAttributeResult<TAttribute> : SanitizationAttributeResult<TAttribute> where TAttribute : BaseSanitizationAttribute
{
    public ParameterSanitizationAttributeResult(TAttribute? attribute) : base(attribute)
    { }
    
    public static implicit operator TAttribute?(ParameterSanitizationAttributeResult<TAttribute> result) => result.SanitizationAttribute;
    public static implicit operator ParameterSanitizationAttributeResult<TAttribute>(TAttribute? result) => new(result);
}