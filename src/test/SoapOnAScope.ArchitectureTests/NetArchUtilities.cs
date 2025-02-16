using FluentAssertions;
using NetArchTest.Rules;

namespace SoapOnAScope.ArchitectureTests;

public static class NetArchUtilities
{
    public static void Check(this ConditionList conditionList) =>
        conditionList.GetResult().IsSuccessful.Should().BeTrue();
}
