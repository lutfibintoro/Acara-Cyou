namespace WebServer.Features.File.UpdateFile
{
    public record UpdateFileResponse
    (
        string FullPathUrl,
        string MimeType,
        ulong ByteLength
    );
}
