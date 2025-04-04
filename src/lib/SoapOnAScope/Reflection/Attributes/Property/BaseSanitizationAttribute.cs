using System.Reflection;

namespace SoapOnAScope;

/// <summary>
///     Internal base type for all sanitization attributes.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public abstract class BaseSanitizationAttribute : Attribute
{
}