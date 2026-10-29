using System.ComponentModel.DataAnnotations;

namespace WebServer.Features.TicketSlice.TicketsDashboard
{
    public class TicketsDashboardResponse
    {
        [Required]
        public Guid TicketId { get; set; }
        [Required]
        public Guid EventId { get; set; }
        [Required]
        public string Category { get; set; } = string.Empty;
        [Required]
        public string ParticipantNotice { get; set; } = string.Empty;
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
        [Required]
        public bool IsDeleted { get; set; }
    }
}
