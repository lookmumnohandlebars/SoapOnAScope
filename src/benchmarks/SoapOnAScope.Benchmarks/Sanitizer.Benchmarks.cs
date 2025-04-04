using BenchmarkDotNet.Attributes;
using SoapOnAScope.Reflection.Models;

namespace SoapOnAScope.Benchmarks;

public class SanitizerBenchmark
{
    [Benchmark]
    public void SanitizeString()
    {
        var input = " <script>alert('XSS')</script>  ";
        Sanitizer.SanitizeString(ref input, new SanitizationMetaData(trim: true));
    }

    // [Benchmark]
    // public void SanitizeClass_WithTenProperties()
    // {
    //     var input = " <script>alert('XSS')</script>  ";
    // }
}
