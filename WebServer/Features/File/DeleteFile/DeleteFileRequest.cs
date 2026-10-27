using FastEndpoints;

namespace WebServer.Features.File.DeleteFile
{
    public record DeleteFileRequest
    (
        [property: FromBody]
        Body Body
    );

    public record Body
    (
        string FileName
    );
}
