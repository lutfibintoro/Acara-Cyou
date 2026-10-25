using FluentResults;
using MediatR;

namespace WebServer.Features.File.UploadFile
{
    public record UploadFileCommand
    (
        string userId,
        IFormFile File
    ) : IRequest<Result<UploadFileResponse>>;
}
