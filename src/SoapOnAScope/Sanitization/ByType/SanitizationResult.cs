namespace SoapOnAScope;

internal record struct SanitizationResult<T>(T Model, bool DidSanitize);
