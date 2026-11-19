using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using FastEndpoints;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.WebUtilities;

namespace WebServer.Features.Paymentso.CreateOrder
{
    public class CreateOrderEndpoint : Ep.Req<CreateOrderRequest>.Res<CreateOrderResponse>
    {
        private readonly ILogger<CreateOrderEndpoint> _logger;
        private readonly ISender _sender;

        public CreateOrderEndpoint(ILogger<CreateOrderEndpoint> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        public override void Configure()
        {
            Post("/api/payment/order");
        }

        public override async Task HandleAsync(CreateOrderRequest req, CancellationToken ct)
        {
            string userIdString = User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).First().Value;
            Guid userId = Guid.Parse(userIdString);

            byte[] byteTeks = Convert.FromBase64String(req.Body.FormTemplateValueBase64);
            string formTemplateValue = Encoding.UTF8.GetString(byteTeks);

            Result<CreateOrderResponse> commandResult = await _sender.Send(new CreateOrderCommand()
            {
                UserId = userId,
                EventId = req.Query.EventId,
                TicketId = req.Body.TicketId,
                PaymentGatewayId = req.Body.PaymentGatewayId,
                PaymentMethodId = req.Body.PaymentMethodId,
                UserEmailAddress = req.Body.UserEmailAddress,
                FormTemplateValue = formTemplateValue
            }, ct);

            if (commandResult.IsFailed)
            {
                Infrastructures.ResponseError.ProblemDetails problemDetails = MapProblem(commandResult, HttpContext);
                await Send.StringAsync(JsonSerializer.Serialize(problemDetails), problemDetails.Status, "application/problem+json", cancellation: ct);
                return;
            }

            Ok<CreateOrderResponse> result = TypedResults.Ok(commandResult.Value);
            await Send.ResultAsync(result);
        }

        #region failed mapping
        private static Infrastructures.ResponseError.ProblemDetails MapProblem(Result<CreateOrderResponse> result, HttpContext httpContext)
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