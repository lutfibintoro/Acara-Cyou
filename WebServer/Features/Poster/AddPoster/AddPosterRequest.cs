using FastEndpoints;

namespace WebServer.Features.Poster.AddPoster
{
    public record AddPosterRequest
    (
        [property: FromBody]
        Body Body
    );

    public record Body
    (
        Guid EventId,
        string FullPathUrl,
        string MimeType,
        ulong ByteLength
    );
}
