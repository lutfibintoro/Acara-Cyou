using System.ComponentModel.DataAnnotations;
using FastEndpoints;

namespace WebServer.Features.TicketSlice.GetTickets
{
    public class GetTicketsRequest
    {
        [Required, RouteParam]
        public Guid EventId { get; set; }
    }
}
