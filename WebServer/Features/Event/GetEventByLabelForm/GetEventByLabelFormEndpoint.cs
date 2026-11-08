using System.Diagnostics;
using System.Text.Json;
using FastEndpoints;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.WebUtilities;

namespace WebServer.Features.Event.GetEventByLabelForm
{
    public class GetEventByLabelFormEndpoint : Ep.Req<GetEventByLabelFormRequest>.Res<GetEventByLabelFormResponse>
    {
        private readonly ILogger<GetEventByLabelFormEndpoint> _logger;
        private readonly ISender _sender;

        public GetEventByLabelFormEndpoint(ILogger<GetEventByLabelFormEndpoint> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        public override void Configure()
        {
            Get("/api/event/form");
        }

        public override async Task HandleAsync(GetEventByLabelFormRequest req, CancellationToken ct)
        {
            Result<GetEventByLabelFormResponse> queryResult = await _sender.Send(new GetEventByLabelFormQuery()
            {
                CustomUrlName = req.Query.CustomUrlName
            }, ct);

            if (queryResult.IsFailed)
            {
                Infrastructures.ResponseError.ProblemDetails problemDetails = MapProblem(queryResult, HttpContext);
                await Send.StringAsync(JsonSerializer.Serialize(problemDetails), problemDetails.Status, "application/problem+json", cancellation: ct);
                return;
            }

            Ok<GetEventByLabelFormResponse> result = TypedResults.Ok(queryResult.Value);
            await Send.ResultAsync(result);
        }

        #region failed mapping
        private static Infrastructures.ResponseError.ProblemDetails MapProblem(Result<GetEventByLabelFormResponse> result, HttpContext httpContext)
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
