using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using FastEndpoints;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.WebUtilities;

namespace WebServer.Features.Event.GetEvents
{
    public class GetEventsEndpoint : Ep.NoReq.Res<List<GetEventsResponse>>
    {
        private readonly ILogger<GetEventsEndpoint> _logger;
        private readonly ISender _sender;

        public GetEventsEndpoint(ILogger<GetEventsEndpoint> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        public override void Configure()
        {
            Get("/api/event");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            string userIdString = User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).First().Value;
            Guid userId = Guid.Parse(userIdString);

            Result<List<GetEventsResponse>> commandResult = await _sender.Send(new GetEventsQuery(userId), ct);

            if (commandResult.IsFailed)
            {
                Infrastructures.ResponseError.ProblemDetails problemDetails = MapProblem(commandResult, HttpContext);
                await Send.StringAsync(JsonSerializer.Serialize(problemDetails), problemDetails.Status, "application/problem+json", cancellation: ct);
                return;
            }

            Ok<List<GetEventsResponse>> result = TypedResults.Ok(commandResult.Value);
            await Send.ResultAsync(result);
        }

        #region failed mapping
        private static Infrastructures.ResponseError.ProblemDetails MapProblem(Result<List<GetEventsResponse>> result, HttpContext httpContext)
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