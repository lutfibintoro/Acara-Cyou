namespace WebServer.Shared.Payments.CreatePayment
{
    public class PaymentGatewayCreateRequest
    {
        public int Amount { get; set; }
        public string OrderId { get; set; } = string.Empty;
        public string ProductDetail { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CodePayment { get; set; } = string.Empty;
        public string CustomerVaName { get; set; } = string.Empty;
        public int ExpiredInMinute { get; set; }
        public string CallbackUrl { get; set; } = string.Empty;
    }
}
