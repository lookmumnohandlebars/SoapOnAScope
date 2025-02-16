using BenchmarkDotNet.Attributes;

namespace SoapOnAScope.Benchmarks;

public class SanitizerBenchmark
{
    [Benchmark]
    public void SanitizeString()
    {
        var input = " <script>alert('XSS')</script>  ";
        Sanitizer.Sanitize(
            ref input,
            new SanitizationSpecification(Trim: true, HtmlEncode: true, JsEncode: true)
        );
    }

    [Benchmark]
    public void SanitizeClass_WithTenProperties()
    {
        var input = " <script>alert('XSS')</script>  ";
        Sanitizer.Sanitize(
            ref input,
            new SanitizationSpecification(Trim: true, HtmlEncode: true, JsEncode: true)
        );
    }
}
