namespace WebServer.Features.Event.UpdateEventById
{
    public record UpdateEventByIdResponse
    (
        Guid EventId,
        string Title
    );
}
