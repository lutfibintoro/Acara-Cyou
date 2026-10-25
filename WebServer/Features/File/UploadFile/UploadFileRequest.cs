using FastEndpoints;

namespace WebServer.Features.File.UploadFile
{
    public record UploadFileRequest
    (
        [property: FromForm]
        Body Body
    );

    public record Body
    (
        IFormFile File
    );
}
