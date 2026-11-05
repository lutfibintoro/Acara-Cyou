using System.Text.Json.Serialization;
using WebServer.Infrastructures.Data.Entities;

namespace WebServer.Features.Paymentso.GetMethod
{
    public class GetMethodResponse
    {
        public Guid PaymentGatewayId { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; } = [];
    }

    public class PaymentMethod
    {
        public Guid PaymentMethodId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethodType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal FlatFee { get; set; }
        public decimal PercentageFee { get; set; }
        public decimal ApplicationFlatFee { get; set; }
    }
}
