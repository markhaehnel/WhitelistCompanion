using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WhitelistCompanion.Configuration;
using WhitelistCompanion.Models;

namespace WhitelistCompanion.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public sealed class ApiKeyAuthorizationAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            if (next is null) throw new ArgumentNullException(nameof(next));

            var apiConfig = context.HttpContext.RequestServices.GetRequiredService<IOptions<ApiConfiguration>>().Value;
            var apiKey = apiConfig.Key;

            var hasApiKey = context.HttpContext.Request.Headers.TryGetValue(Constants.ApiKeyHeaderName, out var extractedApiKey);

            if (!hasApiKey)
            {
                context.Result = BuildResult($"{Constants.ApiKeyHeaderName} was not provided");
                return;
            }

            if (!extractedApiKey.Equals(apiKey))
            {
                context.Result = BuildResult($"{Constants.ApiKeyHeaderName} was not provided");
                return;
            }

            await next();
        }

        private static JsonResult BuildResult(string errorMessage)
        {
            var result = new JsonResult(new ApiResponse<object>()
            {
                Error = errorMessage
            })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
            return result;
        }
    }
}
