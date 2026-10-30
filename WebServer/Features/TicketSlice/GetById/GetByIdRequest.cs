using System.ComponentModel.DataAnnotations;
using FastEndpoints;

namespace WebServer.Features.TicketSlice.GetById
{
    public class GetByIdRequest
    {
        [Required, RouteParam]
        public Guid TicketId { get; set; }
    }
}
