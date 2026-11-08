using System.Diagnostics;
using System.Text.Json;
using FastEndpoints;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.WebUtilities;

namespace WebServer.Features.Event.GetEventByLabelPage
{
    public class GetEventByLabelPageEndpoint : Ep.Req<GetEventByLabelPageRequest>.Res<GetEventByLabelPageResponse>
    {
        private readonly ILogger<GetEventByLabelPageEndpoint> _logger;
        private readonly ISender _sender;

        public GetEventByLabelPageEndpoint(ILogger<GetEventByLabelPageEndpoint> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        public override void Configure()
        {
            Get("/api/event/page");
        }

        public override async Task HandleAsync(GetEventByLabelPageRequest req, CancellationToken ct)
        {
            Result<GetEventByLabelPageResponse> queryResult = await _sender.Send(new GetEventByLabelPageQuery()
            {
                CustomUrlName = req.Query.CustomUrlName
            }, ct);

            if (queryResult.IsFailed)
            {
                Infrastructures.ResponseError.ProblemDetails problemDetails = MapProblem(queryResult, HttpContext);
                await Send.StringAsync(JsonSerializer.Serialize(problemDetails), problemDetails.Status, "application/problem+json", cancellation: ct);
                return;
            }

            Ok<GetEventByLabelPageResponse> result = TypedResults.Ok(queryResult.Value);
            await Send.ResultAsync(result);
        }

        #region failed mapping
        private static Infrastructures.ResponseError.ProblemDetails MapProblem(Result<GetEventByLabelPageResponse> result, HttpContext httpContext)
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
