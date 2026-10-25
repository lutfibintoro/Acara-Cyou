using FluentResults;
using MediatR;

namespace WebServer.Features.File.UpdateFile
{
    public record UpdateFileCommand
    (
        string userId,
        IFormFile File,
        string FileName
    ) : IRequest<Result<UpdateFileResponse>>;
}
