using System.Diagnostics;
using System.Text.Json;
using FastEndpoints;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.WebUtilities;

namespace WebServer.Features.GoogleLogin.KeyExchange
{
    // endpoint untuk tukar key menjadi jwt
    public class KeyExchangeEndpoint : Ep.Req<KeyExchangeRequest>.Res<KeyExchangeResponse>
    {
        private readonly ILogger<KeyExchangeEndpoint> _logger;
        private readonly ISender _sender;

        public KeyExchangeEndpoint(ILogger<KeyExchangeEndpoint> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        public override void Configure()
        {
            Get("/api/google-login/key-exchange");
            AllowAnonymous();
        }

        public override async Task HandleAsync(KeyExchangeRequest req, CancellationToken ct)
        {
            Result<KeyExchangeResponse> commandResult = await _sender.Send(new KeyExchangeCommand(req.Query!.Key), ct);
            if (commandResult.IsFailed)
            {
                Infrastructures.ResponseError.ProblemDetails problemDetails = Map(commandResult, HttpContext);
                await Send.StringAsync(JsonSerializer.Serialize(problemDetails), problemDetails.Status, "application/problem+json", cancellation: ct);
                return;
            }

            Ok<KeyExchangeResponse> result = TypedResults.Ok(commandResult.Value);
            await Send.ResultAsync(result);
            return;
        }

        #region failed mapping
        private static Infrastructures.ResponseError.ProblemDetails Map(Result<KeyExchangeResponse> result, HttpContext httpContext)
        {
            IError[] errorResults = result.Errors.ToArray();

            var errors = new Infrastructures.ResponseError.Error[errorResults.Length];

            for(int i = 0; i < errorResults.Length; i++)
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
