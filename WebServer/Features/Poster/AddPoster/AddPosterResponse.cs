namespace WebServer.Features.Poster.AddPoster
{
    public record AddPosterResponse
    (
        Guid PosterId,
        string FullPathUrl,
        string MimeType,
        ulong ByteLength
    );
}
