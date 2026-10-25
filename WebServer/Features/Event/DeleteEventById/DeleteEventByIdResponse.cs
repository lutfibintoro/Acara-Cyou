namespace WebServer.Features.Event.DeleteEventById
{
    public record DeleteEventByIdResponse
    (
        Guid EventId,
        bool IsDeleted
    );
}
