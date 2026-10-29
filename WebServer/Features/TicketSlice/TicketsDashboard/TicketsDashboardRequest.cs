using System.ComponentModel.DataAnnotations;
using FastEndpoints;

namespace WebServer.Features.TicketSlice.TicketsDashboard
{
    public class TicketsDashboardRequest
    {
        [Required, RouteParam]
        public Guid EventId { get; set; }
    }
}
