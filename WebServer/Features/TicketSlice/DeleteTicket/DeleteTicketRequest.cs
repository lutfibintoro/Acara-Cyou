using FastEndpoints;

namespace WebServer.Features.TicketSlice.DeleteTicket
{
    public record DeleteTicketRequest
    (
        [property: RouteParam]
        Guid TicketId
    );
}
