using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SoapOnAScope.Web;
using SoapOnAScope.Web.FakeRestApi;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.UseHttpsRedirection();
app.MapPost("test", [SanitizeRequest] ([FromBody] TestRequest request) => Results.Ok(request));
app.MapControllers();

app.Run();

public abstract partial class Program { }
