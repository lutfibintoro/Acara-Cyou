namespace WebServer.Infrastructures.Data.Entities
{
    public class PaymentGateway
    {
        public Guid Id { get; set; }
        public PaymentGatewayName Name { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; } = [];
    }

    public enum PaymentGatewayName
    {
        Duitku
    }
}
