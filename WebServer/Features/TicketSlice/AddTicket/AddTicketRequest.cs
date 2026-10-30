using System.ComponentModel.DataAnnotations;
using FastEndpoints;

namespace WebServer.Features.TicketSlice.AddTicket
{
    public class AddTicketRequest
    {
        [Required, FromBody]
        public Body Body { get; set; } = null!;
    }

    public class Body
    {
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
    }
}
