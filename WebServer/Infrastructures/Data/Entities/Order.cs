using System.ComponentModel.DataAnnotations;

namespace WebServer.Infrastructures.Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal TotalItemPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ApplicationFee { get; set; }
        public decimal GrandTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string FormTemplateValue { get; set; } = string.Empty;
        public bool IsCheckedIn { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [ConcurrencyCheck]
        public int RowVersion { get; set; } = 1;


        public Guid AcaraCyouUserId { get; set; }
        public AcaraCyouUser AcaraCyouUser { get; set; } = null!;


        public Invoice? Invoice { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }

    public enum OrderStatus
    {
        Success,
        Pending,
        Cancelled
    }
}
