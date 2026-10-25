using System.Diagnostics;
using System.Text.Json;
using FastEndpoints;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using WebServer.Features.GoogleLogin.SignOut;

namespace WebServer.Features.GoogleLogin.RefreshToken
{
    public class RefreshTokenEndpoint : Ep.Req<RefreshTokenRequest>.Res<RefreshTokenResponse>
    {
        private readonly ILogger<RefreshTokenEndpoint> _logger;
        private readonly ISender _sender;

        public RefreshTokenEndpoint(ILogger<RefreshTokenEndpoint> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        public override void Configure()
        {
            Post("/api/google-login/refresh-token");
            AllowAnonymous();
            Description(x => x.Accepts<RefreshTokenRequest>("application/json"));
        }

        public override async Task HandleAsync(RefreshTokenRequest req, CancellationToken ct)
        {
            Result<RefreshTokenResponse> commandResult = await _sender.Send(new RefreshTokenCommand(req.Body.RefreshToken), ct);

            if (commandResult.IsFailed)
            {
                Infrastructures.ResponseError.ProblemDetails problemDetails = MapProblem(commandResult, HttpContext);
                await Send.StringAsync(JsonSerializer.Serialize(problemDetails), problemDetails.Status, "application/problem+json", cancellation: ct);
                return;
            }

            var result = TypedResults.Ok(commandResult.Value);
            await Send.ResultAsync(result);
        }

        #region failed mapping
        private static Infrastructures.ResponseError.ProblemDetails MapProblem(Result<RefreshTokenResponse> result, HttpContext httpContext)
        {
            IError[] errorResults = result.Errors.ToArray();

            var errors = new Infrastructures.ResponseError.Error[errorResults.Length];

            for (int i = 0; i < errorResults.Length; i++)
            {
                errors[i] = new Infrastructures.ResponseError.Error()
                {
                    Severity = ((Severity)errorResults[i].Metadata["severity"]).ToString(),
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
