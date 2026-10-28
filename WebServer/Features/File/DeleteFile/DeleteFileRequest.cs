using System.ComponentModel.DataAnnotations;
using FastEndpoints;

namespace WebServer.Features.File.DeleteFile
{
    public record DeleteFileRequest
    (
        [property: FromQuery]
        [property: Required]
        Query? Query
    );

    public record Query
    (
        string FileName
    );
}
