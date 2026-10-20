namespace WebServer.Extensions
{
    public static partial class ApplicationCors
    {
        public static IServiceCollection AddApplicationCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                string[]? allowedOrigins = configuration.GetSection("AllowedCors").Get<string[]>();

                options.AddPolicy(ConstantPolicyKeys.AllowedCors,
                    policy =>
                    {
                        policy.WithOrigins(allowedOrigins ?? [])
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });

                options.AddPolicy(ConstantPolicyKeys.AnyAllow,
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            return services;
        }
    }


    public static partial class ApplicationCors
    {
        public static class ConstantPolicyKeys
        {
            public static string AllowedCors { get; } = "AllowedCors";
            public static string AnyAllow { get; } = "AnyAllow";
        }
    }
}
