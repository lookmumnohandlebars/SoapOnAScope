using System.Reflection;
using SoapOnAScope.Web;

namespace SoapOnAScope.ArchitectureTests;

public static class Assemblies
{
    public static Assembly Main => typeof(Sanitizer).Assembly;
    public static Assembly Web => typeof(SanitizeRequestAttribute).Assembly;

    public static Assembly[] All => [Main, Web];
}
