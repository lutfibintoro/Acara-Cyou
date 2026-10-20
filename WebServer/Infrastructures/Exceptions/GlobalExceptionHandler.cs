using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.WebUtilities;
using WebServer.Infrastructures.ResponseError;

namespace WebServer.Infrastructures.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;


        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }


        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ProblemDetails mapEx = MapException(exception, httpContext);

            httpContext.Response.StatusCode = mapEx.Status;
            httpContext.Response.ContentType = "application/problem+json";
            await httpContext.Response.WriteAsJsonAsync(mapEx, cancellationToken);

            return true;
        }


        private static ProblemDetails MapException(Exception exception, HttpContext httpContext)
        {
            return exception switch
            {
                _ => new ProblemDetails()
                {
                    Status = 500,
                    Title = ReasonPhrases.GetReasonPhrase(500),
                    Detail = $"An unexpected error occured, our team has been notified and will troubleshoot the issue",
                    Instance = $"{httpContext.Request.Path}",
                    Type = new Uri("https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/500"),
                    TraceId = $"{Activity.Current?.Id ?? httpContext.TraceIdentifier}",
                    Errors = [new Error()]
                }
            };
        }
    }
}
