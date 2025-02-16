using System.Reflection;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SoapOnAScope.Web;

/// <summary>
///     Automatically deeply sanitizes all string properties of request parameters for a controller
///     or minimal API, method.
/// </summary>
/// <code>
///     public class MyController : ControllerBase {
///
///       [HttpPost]
///       [SanitizeRequest]
///       public IActionResult MyMethod([FromBody] MyRequest request, [FromQuery] string name) {
///         _service.DoSomething(request, name);
///         return Accepted();
///       }
///     }
/// </code>
/// <remarks>
///     This is
/// </remarks>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
public class SanitizeRequestAttribute : ActionFilterAttribute
{
    /// <summary>
    ///     On Action Executing is automatically
    /// </summary>
    /// <param name="actionContext"></param>
    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        foreach (var requestParam in actionContext.ActionArguments)
        {
            //TODO: Make this align with
            var properties =
                requestParam
                    .Value?.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(x =>
                        x is { CanRead: true, CanWrite: true }
                        && x.PropertyType == typeof(string)
                        && x.GetGetMethod(true)!.IsPublic
                        && x.GetSetMethod(true)!.IsPublic
                    ) ?? new List<PropertyInfo>();
            foreach (var propertyInfo in properties)
            {
                propertyInfo.SetValue(
                    requestParam.Value,
                    HttpUtility.HtmlEncode(propertyInfo.GetValue(requestParam.Value) as string)
                );
            }
        }
    }
}
