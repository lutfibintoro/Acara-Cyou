using System.Diagnostics;
using System.Text.Json;
using FastEndpoints;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.WebUtilities;
using WebServer.Features.GoogleLogin.KeyExchange;

namespace WebServer.Features.GoogleLogin.SignOut
{
    public class SignOutEndpoint : Ep.Req<SignOutRequest>.NoRes
    {
        private readonly ILogger<SignOutRequest> _logger;
        private readonly ISender _sender;
        private readonly string _frontendBaseAddress;

        public SignOutEndpoint(ILogger<SignOutRequest> logger, ISender sender, IConfiguration configuration)
        {
            _logger = logger;
            _sender = sender;
            _frontendBaseAddress = configuration["FrontendBaseAddress"]!;
        }

        public override void Configure()
        {
            Delete("/api/google-login/sign-out");
        }

        public override async Task HandleAsync(SignOutRequest req, CancellationToken ct)
        {
            Result commandResult = await _sender.Send(new SignOutCommand(req.Query!.RefreshToken), ct);

            if (commandResult.IsFailed)
            {
                Infrastructures.ResponseError.ProblemDetails problemDetails = MapProblem(commandResult, HttpContext);
                await Send.StringAsync(JsonSerializer.Serialize(problemDetails), problemDetails.Status, "application/problem+json", cancellation: ct);
                return;
            }

            RedirectHttpResult result = TypedResults.Redirect(_frontendBaseAddress, false, false);
            await Send.ResultAsync(result);
        }

        #region failed mapping
        private static Infrastructures.ResponseError.ProblemDetails MapProblem(Result result, HttpContext httpContext)
        {
            IError[] errorResults = result.Errors.ToArray();

            var errors = new Infrastructures.ResponseError.Error[errorResults.Length];

            for (int i = 0; i < errorResults.Length; i++)
            {
                errors[i] = new Infrastructures.ResponseError.Error()
                {
                    Severity = ((Infrastructures.ResultError.Severity)errorResults[i].Metadata["severity"]).ToString(),
                    Reason = errorResults[i].Message,
                    Code = (string)errorResults[i].Metadata["code"],
                    Name = (string)errorResults[i].Metadata["fieldName"]

                };
            }

            return new Infrastructures.ResponseError.ProblemDetails()
            {
                Status = (int)errorResults[0].Metadata["status"],
                Title = ReasonPhrases.GetReasonPhrase((int)errorResults[0].Metadata["status"]),
                Detail = $"",
                Instance = $"{httpContext.Request.Path}",
                Type = new Uri($"https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/{(int)errorResults[0].Metadata["status"]}"),
                TraceId = $"{Activity.Current?.Id ?? httpContext.TraceIdentifier}",
                Errors = errors
            };
        }
        #endregion
    }
}
