using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebServer.Extensions
{
    public static partial class ApplicationAuth
    {
        public static IServiceCollection AddApplicationAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Authentication:Issuer"],
                    ValidAudience = configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Authentication:Token"]!))
                };
            })
            .AddCookie(ConstantAuthScheme.ExternalAuthenticationCookie, options =>
            {
                options.Cookie.Name = ConstantAuthScheme.ExternalAuthenticationCookie;
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };
            })
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"]!;
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"]!;
                options.SignInScheme = ConstantAuthScheme.ExternalAuthenticationCookie;
                options.CallbackPath = "/signin-google";
                options.RemoteAuthenticationTimeout = TimeSpan.FromMinutes(5);
                options.CorrelationCookie.Expiration = TimeSpan.FromMinutes(5);
                options.Events.OnRemoteFailure = context =>
                {
                    context.HandleResponse();
                    context.Response.Redirect("/api/google-login/challenge?returnUrl=%2F", false, false);
                    return Task.CompletedTask;
                };
                options.ClaimActions.MapJsonKey("urn:google:picture", "picture");
            });

            return services;
        }
    }


    public static partial class ApplicationAuth
    {
        public static class ConstantAuthScheme
        {
            public static string ExternalAuthenticationCookie { get; } = "ExternalAuthenticationCookie";
        }
    }
}