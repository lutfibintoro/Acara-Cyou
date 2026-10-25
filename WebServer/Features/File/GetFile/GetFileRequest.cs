using FastEndpoints;

namespace WebServer.Features.File.GetFile
{
    public record GetFileRequest
    (
        [property: FromQuery]
        Query? Query
    );

    public record Query
    (
        string FileName
    );
}
