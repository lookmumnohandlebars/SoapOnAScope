namespace SoapOnAScope;

internal record struct SanitizationAttributeResult(
    bool IsEnabled,
    SanitizationSpecification Specification
);
