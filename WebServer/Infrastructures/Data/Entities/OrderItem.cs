using System.ComponentModel.DataAnnotations;

namespace WebServer.Infrastructures.Data.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal PriceOne { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ConcurrencyCheck]
        public int RowVersion { get; set; } = 1;


        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;


        public Guid TicketCategoryId { get; set; }
        public TicketCategory TicketCategory { get; set; } = null!;
    }
}
