using WebServer.Infrastructures.Exceptions;

namespace WebServer.Extensions
{
    public static class ApplicationException
    {
        public static IServiceCollection AddApplicationException(this IServiceCollection services)
        {
            services.AddProblemDetails();
            services.Configure<ProblemDetailsOptions>(options =>
            {
                options.CustomizeProblemDetails = context =>
                {
                    context.HttpContext.Response.ContentType = "application/problem+json";
                };
            });

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.Configure<ExceptionHandlerOptions>(options =>
            {
                options.SuppressDiagnosticsCallback = context => false;
            });

            return services;
        }
    }
}
