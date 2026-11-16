using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.Data.Entities;

namespace WebServer.Extensions
{
    public static class ApplicationDbSetup
    {
        public static IServiceCollection AddApplicationDbSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AcaraCyouDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("MariadbConnection"), new MariaDbServerVersion(new Version(12, 3, 3)))
                    .UseSnakeCaseNamingConvention();
            });

            services.AddIdentityCore<AcaraCyouUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890_-+= \\|/?.>,<:;'\"[]{})(*&^%$#@!~`";
            })
            .AddRoles<AcaraCyouRole>()
            .AddEntityFrameworkStores<AcaraCyouDbContext>();

            return services;
        }

        public static void MigrateDbSetup(WebApplication app)
        {
            using (IServiceScope scope = app.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                AcaraCyouDbContext context = services.GetRequiredService<AcaraCyouDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
