using System.Diagnostics;
using FastEndpoints;
using FastEndpoints.OpenApi;
using FluentValidation;
using Microsoft.AspNetCore.WebUtilities;
using Scalar.AspNetCore;
using WebServer.Extensions;

namespace WebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddFastEndpoints();
            builder.Services.AddMemoryCache();
            builder.Services.OpenApiDocument(o => o.AutoTagPathSegmentIndex = 2);

            // application extension
            builder.Services.AddApplicationDbSetup(builder.Configuration);
            builder.Services.AddApplicationCors(builder.Configuration);
            builder.Services.AddApplicationAuth(builder.Configuration);
            builder.Services.AddApplicationException();
            builder.Services.AddApplicationStatusCodePage();

            

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseExceptionHandler();
            app.UseStatusCodePages();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(ApplicationCors.ConstantPolicyKeys.AllowedCors);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseFastEndpoints(config =>
            {
                config.Errors.UseProblemDetails(problemDetailsConfig =>
                {
                    problemDetailsConfig.ResponseBuilder = (validationFailure, httpContext, statusCode) =>
                    {
                        var errors = new Infrastructures.ResponseError.Error[validationFailure.Count];
                        for (int i = 0; i < validationFailure.Count; i++)
                        {
                            errors[i] = new Infrastructures.ResponseError.Error()
                            {
                                Name = validationFailure[i].PropertyName,
                                Reason = validationFailure[i].ErrorMessage,
                                Code = (string)new Infrastructures.ResultError.ValidationError<string>("", "", null, Severity.Error).Metadata["code"],
                                Severity = "Error"
                            };
                        }

                        return new Infrastructures.ResponseError.ProblemDetails()
                        {
                            Status = statusCode,
                            Title = ReasonPhrases.GetReasonPhrase(statusCode),
                            Detail = "",
                            Instance = $"{httpContext.Request.Path}",
                            Type = new Uri($"https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/{statusCode}"),
                            TraceId = $"{Activity.Current?.Id ?? httpContext.TraceIdentifier}",
                            Errors = errors
                        };
                    };
                });
            });
            app.Run();
        }
    }
}
