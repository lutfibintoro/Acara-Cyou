namespace WebServer.Infrastructures.Data.Entities
{
    public class PaymentMethod
    {
        public Guid Id { get; set; }
        public PaymentMethodType Type { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int MaxExpiredInMinute { get; set; }
        public decimal FlatFee { get; set; }
        public decimal PercentageFee { get; set; }
        public PaymentMethodAdminFeeBearer AdminFeeBearer { get; set; }

        public Guid PaymentGatewayId { get; set; }
        public PaymentGateway PaymentGateway { get; set; } = null!;
    }

    public enum PaymentMethodType
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

    public enum PaymentMethodAdminFeeBearer
    {
        Merchant,
        Customer
    }
}
