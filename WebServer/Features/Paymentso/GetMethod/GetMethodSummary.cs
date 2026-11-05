using System.Text.Json.Serialization;
using FastEndpoints;

namespace WebServer.Features.Paymentso.GetMethod
{
    public class GetMethodSummary : Summary<GetMethodEndpoint>
    {
        public GetMethodSummary()
        {
            Response<SummaryGetMethodResponse>(StatusCodes.Status200OK, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }

        public class SummaryGetMethodResponse
        {
            public Guid PaymentGatewayId { get; set; }
            public List<SummaryPaymentMethod> PaymentMethods { get; set; } = [];
        }

        public class SummaryPaymentMethod
        {
            public Guid PaymentMethodId { get; set; }
            public Type Type { get; set; }
            public string Name { get; set; } = string.Empty;
            public decimal FlatFee { get; set; }
            public decimal PercentageFee { get; set; }
            public decimal ApplicationFlatFee { get; set; }
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Type
        {
            Qris,
            VirtualAccount,
            CreditCard,
            Retail,
            EWallet,
            EBanking,
            ECommerce,
            Credit
        }
    }
}
