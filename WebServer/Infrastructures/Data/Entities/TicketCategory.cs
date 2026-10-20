using System.ComponentModel.DataAnnotations;

namespace WebServer.Infrastructures.Data.Entities
{
    public class TicketCategory
    {
        public Guid Id { get; set; }
        public string Category { get; set; } = string.Empty;
        public decimal PriceOne { get; set; }
        public int Quota { get; set; }
        public int BookedQuota { get; set; }
        public bool IsSalesOpen { get; set; } = false;
        public DateTime OpenSalesDate { get; set; } = DateTime.UtcNow;
        public DateTime CloseSalesDate { get; set; } = DateTime.UtcNow + TimeSpan.FromMinutes(1);
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ConcurrencyCheck]
        public int RowVersion { get; set; } = 1;


        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;


        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
