using System.ComponentModel.DataAnnotations;

namespace WebServer.Infrastructures.Data.Entities
{
    public class PaymentDetail
    {
        public Guid Id { get; set; }
        public string PgProvider { get; set; } = string.Empty;
        public string PgTransactionId { get; set; } = string.Empty;
        public string PgReference { get; set; } = string.Empty;
        public string PgMethod { get; set; } = string.Empty;
        public decimal PgFee { get; set; }
        public decimal PaidTotal { get; set; }
        public string Va { get; set; } = string.Empty;
        public string QrString { get; set; } = string.Empty;
        public PaymentDetailStatus Status { get; set; } = PaymentDetailStatus.Pending;
        public PaymentDetailAdminFeeBearer AdminFeeBearer { get; set; }
        public string RawResponse { get; set; } = string.Empty;
        public DateTime? CallbackAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [ConcurrencyCheck]
        public int RowVersion { get; set; } = 1;


        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;
    }

    public enum PaymentDetailStatus
    {
        Settlement,
        Pending,
        Deny,
        Expired
    }

    public enum PaymentDetailAdminFeeBearer
    {
        Merchant,
        Customer
    }
}
