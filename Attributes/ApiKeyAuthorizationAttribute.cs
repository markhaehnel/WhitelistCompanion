using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WhitelistCompanion.Configuration;
using WhitelistCompanion.Utils;

namespace WhitelistCompanion.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class ApiKeyAuthorizationAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            if (next is null) throw new ArgumentNullException(nameof(next));

            var apiConfig = context.HttpContext.RequestServices.GetRequiredService<IOptions<ApiConfiguration>>().Value;
            var apiKey = apiConfig.Key;

            var hasApiKey = context.HttpContext.Request.Headers.TryGetValue(Constants.ApiKeyHeaderName, out var extractedApiKey)
                            || context.HttpContext.Request.Query.TryGetValue(Constants.ApiKeyHeaderName, out extractedApiKey);

            if (!hasApiKey)
            {
                context.Result = JsonResultBuilder.BuildResult($"{Constants.ApiKeyHeaderName} was not provided", StatusCodes.Status401Unauthorized);
                return;
            }

            if (!extractedApiKey.Equals(apiKey))
            {
                context.Result = JsonResultBuilder.BuildResult($"{Constants.ApiKeyHeaderName} was not provided", StatusCodes.Status401Unauthorized);
                return;
            }

            await next();
        }
    }
}
