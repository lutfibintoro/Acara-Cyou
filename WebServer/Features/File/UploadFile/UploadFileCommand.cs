using FluentResults;
using MediatR;

namespace WebServer.Features.File.UploadFile
{
    public record UploadFileCommand
    (
        IFormFile File
    ) : IRequest<Result<UploadFileResponse>>;
}
