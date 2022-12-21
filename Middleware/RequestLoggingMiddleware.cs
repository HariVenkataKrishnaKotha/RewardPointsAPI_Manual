using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace RewardPointsAPI_Manual.Middleware
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Log the request
            _logger.LogDebug($"{context.Request.Method} {context.Request.Path}");

            // Execute the next middleware in the pipeline
            await next(context);
        }
    }
}
