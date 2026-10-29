using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace WebServer.Features.TicketSlice.UpdateTicket
{
    public class UpdateTicketCommand : IRequest<Result>
    {
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }
        public string Category { get; set; } = string.Empty;
        public string ParticipantNotice { get; set; } = string.Empty;
        public decimal PriceOne { get; set; }
        public int Quota { get; set; }
        public int BookedQuota { get; set; }
        public bool IsSalesOpen { get; set; }
        public DateTime OpenSalesDate { get; set; }
        public DateTime CloseSalesDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
