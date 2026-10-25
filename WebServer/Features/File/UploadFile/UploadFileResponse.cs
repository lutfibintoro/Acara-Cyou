namespace WebServer.Features.File.UploadFile
{
    public record UploadFileResponse
    (
        string FullPathUrl,
        string MimeType,
        ulong ByteLength
    );
}
