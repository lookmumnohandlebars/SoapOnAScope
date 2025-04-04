namespace SoapOnAScope.Reflection.Models;

internal class SanitizationAttributeResult<TAttribute> : IEquatable<SanitizationAttributeResult<TAttribute>>
{
    public SanitizationAttributeResult(TAttribute? sanitizationAttribute)
    {
        SanitizationAttribute = sanitizationAttribute;
    }

    public TAttribute? SanitizationAttribute { get; }
    public bool Exists => SanitizationAttribute is not null;
    
    public static implicit operator TAttribute?(SanitizationAttributeResult<TAttribute> result) => result.SanitizationAttribute;
    public static implicit operator SanitizationAttributeResult<TAttribute>(TAttribute? result) => new(result);
    
    public bool Equals(SanitizationAttributeResult<TAttribute>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<TAttribute?>.Default.Equals(SanitizationAttribute, other.SanitizationAttribute);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SanitizationAttributeResult<TAttribute>)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TAttribute?>.Default.GetHashCode(SanitizationAttribute);
    }
}