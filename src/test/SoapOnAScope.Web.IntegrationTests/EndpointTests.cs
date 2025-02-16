using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SoapOnAScope.Web.FakeRestApi;

namespace SoapOnAScope.Web.IntegrationTests;

public class EndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public EndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Test1()
    {
        var client = _factory.CreateClient();
        var res = await client.PostAsJsonAsync("test", new TestRequest() { Message = " TRIM ME " });
        var responseModel = await res.Content.ReadFromJsonAsync<TestRequest>();
        responseModel!.Message.Should().Be("TRIM ME");
    }
}
