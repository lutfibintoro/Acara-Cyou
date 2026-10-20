using System.ComponentModel.DataAnnotations;

namespace WebServer.Infrastructures.Data.Entities
{
    public class DisbursementDetail
    {
        public Guid Id { get; set; }
        public string DisbursementAccountNumber { get; set; } = string.Empty;
        public string DisbursementAccountName { get; set; } = string.Empty;
        public decimal DisbursementAmount { get; set; }
        public decimal DisbursementFee { get; set; }
        public string DisbursementbankCode { get; set; } = string.Empty;
        public string DisbursementbankIssuer { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ConcurrencyCheck]
        public int RowVersion { get; set; } = 1;


        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;
    }
}
