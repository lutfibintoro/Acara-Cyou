using FastEndpoints;

namespace WebServer.Features.Event.DeleteEventById
{
    public record DeleteEventByIdRequest
    (
        [property: QueryParam]
        Guid EventId
    );
}
