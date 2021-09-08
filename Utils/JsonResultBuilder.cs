using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhitelistCompanion.Models;

namespace WhitelistCompanion.Utils
{
    public static class JsonResultBuilder
    {
        public static JsonResult BuildResult(string errorMessage, int statusCode)
        {
            var result = new JsonResult(new ApiResponse<object>()
            {
                Error = errorMessage
            })
            {
                StatusCode = statusCode
            };
            return result;
        }
    }
}
