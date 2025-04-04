namespace SoapOnAScope;

/// <summary>
///     Internal base type for all sanitization attributes.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public abstract class BaseAutoSanitizationAttribute<T> : Attribute where T : BaseSanitizationAttribute
{
    internal abstract T UnderlyingAttribute { get; }
}