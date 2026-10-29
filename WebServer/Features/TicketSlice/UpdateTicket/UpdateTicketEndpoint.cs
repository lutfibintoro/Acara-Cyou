using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using FastEndpoints;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.WebUtilities;

namespace WebServer.Features.TicketSlice.UpdateTicket
{
    public class UpdateTicketEndpoint : Ep.Req<UpdateTicketRequest>.NoRes
    {
        private readonly ILogger<UpdateTicketEndpoint> _logger;
        private readonly ISender _sender;

        public UpdateTicketEndpoint(ILogger<UpdateTicketEndpoint> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        public override void Configure()
        {
            Put("/api/ticket/{ticketId:guid}");
        }

        public override async Task HandleAsync(UpdateTicketRequest req, CancellationToken ct)
        {
            string userIdString = User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).First().Value;
            Guid userId = Guid.Parse(userIdString);

            Result commandResult = await _sender.Send(new UpdateTicketCommand()
            {
                TicketId = req.TicketId,
                UserId = userId,
                Category = req.Body.Category,
                CloseSalesDate = req.Body.CloseSalesDate,
                BookedQuota = req.Body.BookedQuota,
                IsDeleted = req.Body.IsDeleted,
                IsSalesOpen = req.Body.IsSalesOpen,
                OpenSalesDate = req.Body.OpenSalesDate,
                ParticipantNotice = req.Body.ParticipantNotice,
                PriceOne = req.Body.PriceOne,
                Quota = req.Body.Quota
            }, ct);

            if (commandResult.IsFailed)
            {
                Infrastructures.ResponseError.ProblemDetails problemDetails = MapProblem(commandResult, HttpContext);
                await Send.StringAsync(JsonSerializer.Serialize(problemDetails), problemDetails.Status, "application/problem+json", cancellation: ct);
                return;
            }

            NoContent result = TypedResults.NoContent();
            await Send.ResultAsync(result);
        }

        #region failed mapping
        private static Infrastructures.ResponseError.ProblemDetails MapProblem(Result result, HttpContext httpContext)
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