using System.ComponentModel.DataAnnotations;

namespace WebServer.Features.TicketSlice.GetTickets
{
    public class GetTicketsResponse
    {
        [Required]
        public Guid TicketId { get; set; }
        [Required]
        public string Category { get; set; } = string.Empty;
        [Required]
        public decimal PriceOne { get; set; }
        [Required]
        public int Quota { get; set; }
        [Required]
        public int BookedQuota { get; set; }
        [Required]
        public bool IsSalesOpen { get; set; }
        [Required]
        public DateTime OpenSalesDate { get; set; }
        [Required]
        public DateTime CloseSalesDate { get; set; }
    }
}
