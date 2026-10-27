using FluentResults;
using FluentValidation;
using MediatR;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.File.DeleteFile
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, Result>
    {
        private const string StorageDirectory = "./UserUploads";

        private readonly ILogger<DeleteFileCommandHandler> _logger;

        public DeleteFileCommandHandler(ILogger<DeleteFileCommandHandler> logger)
        {
            _logger = logger;
        }

        public Task<Result> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            string fileName = Path.GetFileName(request.FileName);
            int separatorIndex = fileName.IndexOf('+');

            if (separatorIndex < 0)
            {
                NotFoundError<string> resultError = new("fileName", request.FileName, null, Severity.Error);
                return Task.FromResult(Result.Fail(resultError));
            }

            string fileOwner = fileName[..separatorIndex];

            if (!string.Equals(fileOwner, request.userId, StringComparison.Ordinal))
            {
                ForbiddenError<string> resultError = new("fileName", request.FileName, null, Severity.Error);
                return Task.FromResult(Result.Fail(resultError));
            }

            string targetPath = $"{StorageDirectory}/{fileName}";

            if (!System.IO.File.Exists(targetPath))
            {
                return Task.FromResult(Result.Ok());
            }

            System.IO.File.Delete(targetPath);

            _logger.LogInformation("File [{FileName}] was deleted by user [{userId}]", request.FileName, request.userId);

            return Task.FromResult(Result.Ok());
        }
    }
}
