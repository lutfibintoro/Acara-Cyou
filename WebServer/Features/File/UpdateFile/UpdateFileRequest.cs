using FastEndpoints;

namespace WebServer.Features.File.UpdateFile
{
    public record UpdateFileRequest
    (
        [property: FromForm]
        Body Body
    );

    public record Body
    (
        IFormFile File,
        string FileName
    );
}
