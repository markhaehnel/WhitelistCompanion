using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using WhitelistCompanion.Services;

namespace WhitelistCompanion.Utils
{
    public class ExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        private readonly IWebHostEnvironment _env;

        public ExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));

            if (!_env.IsDevelopment())
            {
                if (context.Exception is not null)
                {
                    context.Result = context.Exception switch
                    {
                        RconUnavailableException ex => JsonResultBuilder.BuildResult(ex.Message, StatusCodes.Status503ServiceUnavailable),
                        _ => JsonResultBuilder.BuildResult("An internal error occured", StatusCodes.Status500InternalServerError),
                    };
                    context.ExceptionHandled = true;
                }
            }
        }
    }
}
