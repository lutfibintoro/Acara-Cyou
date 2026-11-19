using System.ComponentModel.DataAnnotations;
using FastEndpoints;

namespace WebServer.Features.Paymentso.CreateOrder
{
    public class CreateOrderRequest
    {
        [Required, FromQuery]
        public Query Query { get; set; } = null!;
        [Required, FromBody]
        public Body Body { get; set; } = null!;
    }

    public class Query
    {
        [Required]
        public Guid EventId { get; set; }
    }

    public class Body
    {
        [Required]
        public Guid PaymentGatewayId { get; set; }
        [Required]
        public Guid PaymentMethodId { get; set; }
        [Required]
        public Guid TicketId { get; set; }
        [Required]
        public string UserEmailAddress { get; set; } = string.Empty;
        [Required]
        public string FormTemplateValueBase64 { get; set; } = string.Empty;
    }
}
