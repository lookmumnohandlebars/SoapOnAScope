using NetArchTest.Rules;

namespace SoapOnAScope.ArchitectureTests;

public class SdkArchitectureTests
{
    [Fact]
    public void Namespace_ShouldOnlyBePackageName() =>
        Types
            .InAssembly(Assemblies.Main)
            .That()
            .ArePublic()
            .Should()
            .ResideInNamespaceMatching("SoapOnAScope")
            .Check();

    [Fact]
    public void Namespace_ShouldOnlyBePackageNameForWeb() =>
        Types
            .InAssembly(Assemblies.Web)
            .That()
            .ArePublic()
            .Should()
            .ResideInNamespaceMatching(@"^SoapOnAScope\.Web$")
            .Check();
}
