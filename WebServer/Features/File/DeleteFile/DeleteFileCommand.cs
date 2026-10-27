using FluentResults;
using MediatR;

namespace WebServer.Features.File.DeleteFile
{
    public record DeleteFileCommand
    (
        string userId,
        string FileName
    ) : IRequest<Result>;
}
