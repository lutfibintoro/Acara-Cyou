namespace WebServer.Extensions
{
    public static partial class ApplicationHttpClient
    {
        public static IServiceCollection AddApplicationHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient(ConstantHttpName.DuitkuPG, configureClient =>
            {
                configureClient.BaseAddress = new Uri(configuration["PaymentGateway:DuitkuPG:BaseAddress"]!);
                configureClient.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return services;
        }
    }

    public static partial class ApplicationHttpClient
    {
        public static class ConstantHttpName
        {
            public static string DuitkuPG { get; } = "duitku-payment-gateway";
        }
    }
}