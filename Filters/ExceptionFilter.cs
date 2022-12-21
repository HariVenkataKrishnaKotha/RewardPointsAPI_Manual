using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace RewardPointsAPI_Manual.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilter> _logger;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            // Log the exception
            _logger.LogError(context.Exception, context.Exception.Message);

            // Set the response status code
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Set the response content
            context.Result = new JsonResult(new
            {
                Message = "An error occurred while processing your request."
            });
        }
    }
}
