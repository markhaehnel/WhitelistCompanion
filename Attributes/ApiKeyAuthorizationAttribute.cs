using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WhitelistCompanion.Configuration;

namespace WhitelistCompanion.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ApiKeyAuthorizationAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = 401,
                    Content = $"{{ \"success\": false, \"error\": \"{APIKEYNAME} was not provided\" }}"
                };
                return;
            }

            var apiConfig = context.HttpContext.RequestServices.GetRequiredService<IOptions<ApiConfiguration>>().Value;
            var apiKey = apiConfig.Key;

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = 401,
                    Content = $"{{ \"success\": false, \"error\": \"{APIKEYNAME} invalid\" }}"
                };
                return;
            }

            await next();
        }
    }
}
