using System.Reflection;
using NetArchTest.Rules;

namespace SoapOnAScope.ArchitectureTests;

public class SdkRegressionTests
{
    [Fact]
    public Task Public_classes_should_not_unintentionally_regress()
    {
        //Note!: Any non-additive changes should require a new major version
        //Additions should require a new minor version
        var publicTypes = Types.InAssemblies(Assemblies.All).That().ArePublic().GetTypes();
        return Verify(publicTypes.Select(t => t.Name).ToList()).UseDirectory("Snapshots");
    }

    [Fact]
    public Task Methods_on_public_classes_should_not_unintentionally_regress()
    {
        //Note!: Any non-additive changes should require a new major version
        //Additions should require a new minor version
        var methodsByClass = Types
            .InAssemblies(Assemblies.All)
            .That()
            .ArePublic()
            .GetTypes()
            .ToDictionary(type => type.Name, type => type.GetMethods());
        return Verify(methodsByClass).UseDirectory("Snapshots");
    }
}
