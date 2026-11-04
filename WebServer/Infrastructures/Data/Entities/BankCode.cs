namespace WebServer.Infrastructures.Data.Entities
{
    public class BankCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public bool IsBifastSupport { get; set; } = false;
    }
}
