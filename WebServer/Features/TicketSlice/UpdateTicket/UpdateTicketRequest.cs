using System.ComponentModel.DataAnnotations;
using FastEndpoints;

namespace WebServer.Features.TicketSlice.UpdateTicket
{
    public class UpdateTicketRequest
    {
        [Required, RouteParam]
        public Guid TicketId { get; set; }
        [Required, FromBody]
        public Body Body { get; set; } = null!;
    }

    public class Body
    {
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
