using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using FastEndpoints;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.WebUtilities;
using WebServer.Features.CustomUrl.GetCustomUrl;

namespace WebServer.Features.CustomUrl.UpdateCustomUrl
{
    public class UpdateCustomUrlEndpoint : Ep.Req<UpdateCustomUrlRequest>.Res<UpdateCustomUrlResponse>
    {
        private readonly ILogger<UpdateCustomUrlEndpoint> _logger;
        private readonly ISender _sender;

        public UpdateCustomUrlEndpoint(ILogger<UpdateCustomUrlEndpoint> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        public override void Configure()
        {
            Put("/api/custom-url");
        }

        public override async Task HandleAsync(UpdateCustomUrlRequest req, CancellationToken ct)
        {
            string userIdString = User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).First().Value;
            Guid userId = Guid.Parse(userIdString);

            Result<UpdateCustomUrlResponse> queryResult = await _sender.Send(new UpdateCustomUrlCommand()
            {
                UserId = userId,
                EventId = req.Body.EventId,
                FormCustomUrl = req.Body.FormCustomUrl.ToLower(),
                PageCustomUrl = req.Body.PageCustomUrl.ToLower(),
            }, ct);

            if (queryResult.IsFailed)
            {
                Infrastructures.ResponseError.ProblemDetails problemDetails = MapProblem(queryResult, HttpContext);
                await Send.StringAsync(JsonSerializer.Serialize(problemDetails), problemDetails.Status, "application/problem+json", cancellation: ct);
                return;
            }

            Ok<UpdateCustomUrlResponse> result = TypedResults.Ok(queryResult.Value);
            await Send.ResultAsync(result);
        }

        #region failed mapping
        private static Infrastructures.ResponseError.ProblemDetails MapProblem(Result<UpdateCustomUrlResponse> result, HttpContext httpContext)
        {
            IError[] errorResults = [.. result.Errors];

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