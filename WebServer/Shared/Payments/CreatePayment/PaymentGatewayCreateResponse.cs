namespace WebServer.Shared.Payments.CreatePayment
{
    public class PaymentGatewayCreateResponse
    {
        public string MerchantCode { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public int Amount { get; set; }
        public string VaNumber { get; set; } = string.Empty;
        public string QrString { get; set; } = string.Empty;
    }
}
