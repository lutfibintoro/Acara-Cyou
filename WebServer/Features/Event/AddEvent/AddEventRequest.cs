using FastEndpoints;

namespace WebServer.Features.Event.AddEvent
{
    public record AddEventRequest
    (
        [property: FromBody]
        Body Body
    );

    public record Body
    (
        string Title,
        string Description,
        List<Poster>? Posters
    );

    public record Poster
    (
        string FullPathUrl,
        string MimeType,
        ulong ByteLength
    );
}
