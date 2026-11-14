using Hangfire;

namespace WebServer.Extensions
{
    public static class ApplicationHangfireSetup
    {
        public static IServiceCollection AddApplicationHangfireSetup(this IServiceCollection services)
        {
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseInMemoryStorage());

            services.AddHangfireServer();

            return services;
        }
    }
}
