using BenchmarkDotNet.Running;
using SoapOnAScope.Benchmarks;

// dotnet
public static class Program
{
    public static void Main(string[] args) => BenchmarkRunner.Run<SanitizerBenchmark>();
}
