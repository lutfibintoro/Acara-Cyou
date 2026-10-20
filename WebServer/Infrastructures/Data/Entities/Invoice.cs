using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace WebServer.Infrastructures.Data.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string InvoiceNumber { get; set; } = $"INV-{Base64Url.EncodeToString(Guid.NewGuid().ToByteArray())}";
        public decimal AmountDue { get; set; }
        public DateTime DueAt { get; set; } = DateTime.UtcNow + TimeSpan.FromMinutes(1440);
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Unpaid;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [ConcurrencyCheck]
        public int RowVersion { get; set; } = 1;


        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;


        public PaymentDetail? PaymentDetail { get; set; }
    }

    public enum InvoiceStatus
    {
        Unpaid,
        Paid,
        Expired,
        Refunded
    }
}
