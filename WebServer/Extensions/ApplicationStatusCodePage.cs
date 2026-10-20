using System.Diagnostics;
using Microsoft.AspNetCore.WebUtilities;
using WebServer.Infrastructures.ResponseError;

namespace WebServer.Extensions
{
    public static class ApplicationStatusCodePage
    {
        public static IServiceCollection AddApplicationStatusCodePage(this IServiceCollection services)
        {
            services.Configure<StatusCodePagesOptions>(options =>
            {
                options.HandleAsync = async context =>
                {
                    context.HttpContext.Response.ContentType = "application/problem+json";
                    await context.HttpContext.Response.WriteAsJsonAsync(new ProblemDetails()
                    {
                        Status = context.HttpContext.Response.StatusCode,
                        Title = ReasonPhrases.GetReasonPhrase(context.HttpContext.Response.StatusCode),
                        Detail = context.HttpContext.Response.StatusCode < 500 ? "" : "An unexpected error occured, our team has been notified and will troubleshoot the issue",
                        Instance = $"{context.HttpContext.Request.Path}",
                        Type = new Uri($"https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/{context.HttpContext.Response.StatusCode}"),
                        TraceId = $"{Activity.Current?.Id ?? context.HttpContext.TraceIdentifier}",
                        Errors = [new Error()]
                    });
                };
            });
            return services;
        }
    }
}
