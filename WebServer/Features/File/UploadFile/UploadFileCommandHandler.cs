using System.Net;
using FluentResults;
using MediatR;
using static FastEndpoints.Ep;

namespace WebServer.Features.File.UploadFile
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Result<UploadFileResponse>>
    {
        private readonly ILogger<UploadFileCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UploadFileCommandHandler(ILogger<UploadFileCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<UploadFileResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;
            var httpContext = _httpContextAccessor.HttpContext!;

            var extension = Path.GetExtension(file.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";

            Directory.CreateDirectory("./UserUploads");
            var filePath = $"./UserUploads/{uniqueFileName}";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            return Result.Ok(new UploadFileResponse(
                FullPathUrl: $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/file?fileName={WebUtility.UrlEncode(filePath)}",
                MimeType: file.ContentType,
                ByteLength: (ulong)file.Length
            ));
        }
    }
}
