using FluentResults;
using MediatR;

namespace WebServer.Features.Paymentso.CreateOrder
{
    public class CreateOrderCommand : IRequest<Result<CreateOrderResponse>>
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public Guid PaymentGatewayId { get; set; }
        public Guid PaymentMethodId { get; set; }
        public Guid TicketId { get; set; }
        public string UserEmailAddress { get; set; } = string.Empty;
        public string FormTemplateValue { get; set; } = string.Empty;
    }
}
