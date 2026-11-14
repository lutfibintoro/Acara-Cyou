using FluentResults;

namespace WebServer.Shared.Payments.CreatePayment
{
    public interface IPaymentGatewayCreate
    {
        public string PaymentGatewayName { get; }
        public Task<Result<PaymentGatewayCreateResponse>> CreatePaymentAsync(PaymentGatewayCreateRequest request, CancellationToken ct);
    }
}
