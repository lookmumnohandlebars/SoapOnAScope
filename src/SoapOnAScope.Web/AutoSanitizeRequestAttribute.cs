using System.Reflection;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SoapOnAScope.Web;

public class AutoSanitizeRequestAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        foreach (var requestParam in actionContext.ActionArguments)
        {
            //TODO: Make this align with
            var properties = requestParam
                .Value.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x =>
                    x.CanRead
                    && x.CanWrite
                    && x.PropertyType == typeof(string)
                    && x.GetGetMethod(true).IsPublic
                    && x.GetSetMethod(true).IsPublic
                );
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
