using System.Net;
using FluentResults;
using FluentValidation;
using MediatR;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.File.UpdateFile
{
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, Result<UpdateFileResponse>>
    {
        private const string StorageDirectory = "./UserUploads";

        private readonly ILogger<UpdateFileCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateFileCommandHandler(ILogger<UpdateFileCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<UpdateFileResponse>> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            IFormFile file = request.File;

            string fileName = Path.GetFileName(request.FileName);
            int separatorIndex = fileName.IndexOf('+');

            if (separatorIndex < 0)
            {
                NotFoundError<string> resultError = new("fileName", request.FileName, null, Severity.Error);
                return Result.Fail(resultError);
            }

            string fileOwner = fileName[..separatorIndex];

            if (!string.Equals(fileOwner, request.userId, StringComparison.Ordinal))
            {
                ForbiddenError<string> resultError = new("fileName", request.FileName, null, Severity.Error);
                return Result.Fail(resultError);
            }

            string oldTargetPath = $"{StorageDirectory}/{fileName}";

            if (!System.IO.File.Exists(oldTargetPath))
            {
                NotFoundError<string> resultError = new("fileName", request.FileName, null, Severity.Error);
                return Result.Fail(resultError);
            }

            System.IO.File.Delete(oldTargetPath);

            string newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}{Path.GetExtension(file.FileName)}";
            string newTargetPath = $"{StorageDirectory}/{newFileName}";

            Directory.CreateDirectory(StorageDirectory);

            using (FileStream stream = new FileStream(newTargetPath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            HttpContext httpContext = _httpContextAccessor.HttpContext!;
            string fullPathUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/file?fileName={WebUtility.UrlEncode(newTargetPath)}";

            UpdateFileResponse response = new(
                FullPathUrl: fullPathUrl,
                MimeType: file.ContentType,
                ByteLength: (ulong)file.Length
            );

            _logger.LogInformation("File [{OldFileName}] was updated to [{NewFileName}] by user [{userId}]", request.FileName, newFileName, request.userId);

            return Result.Ok(response);
        }
    }
}
