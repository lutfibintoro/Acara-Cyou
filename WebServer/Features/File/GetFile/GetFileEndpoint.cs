using System.Diagnostics;
using System.Text.Json;
using FastEndpoints;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.WebUtilities;
using WebServer.Features.File.Upload;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.File.GetFile
{
    public class GetFileEndpoint : Ep.Req<GetFileRequest>.NoRes
    {
        private readonly ILogger<GetFileEndpoint> _logger;

        public GetFileEndpoint(ILogger<GetFileEndpoint> logger)
        {
            _logger = logger;
        }

        public override void Configure()
        {
            Get("/api/file");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetFileRequest req, CancellationToken ct)
        {
            string fileName = req.Query!.FileName;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out string? contentType);

            if (!System.IO.File.Exists(fileName))
            {
                Result notFound = Result.Fail(new NotFoundError<string>(nameof(fileName), fileName, null, Severity.Error));
                Infrastructures.ResponseError.ProblemDetails problemDetails = MapProblem(notFound, HttpContext);
                await Send.StringAsync(JsonSerializer.Serialize(problemDetails), problemDetails.Status, "application/problem+json", cancellation: ct);
                return;
            }

            using (FileStream stream = System.IO.File.OpenRead(fileName))
            {
                FileStreamHttpResult result = TypedResults.File(stream, contentType);
                await Send.ResultAsync(result);
            }
        }

        #region failed mapping
        private static Infrastructures.ResponseError.ProblemDetails MapProblem(Result result, HttpContext httpContext)
        {
            IError[] errorResults = result.Errors.ToArray();

            var errors = new Infrastructures.ResponseError.Error[errorResults.Length];

            for (int i = 0; i < errorResults.Length; i++)
            {
                errors[i] = new Infrastructures.ResponseError.Error()
                {
                    Severity = ((Severity)errorResults[i].Metadata["severity"]).ToString(),
                    Reason = errorResults[i].Message,
                    Code = (string)errorResults[i].Metadata["code"],
                    Name = (string)errorResults[i].Metadata["fieldName"]

                };
            }

            return new Infrastructures.ResponseError.ProblemDetails()
            {
                Status = (int)errorResults[0].Metadata["status"],
                Title = ReasonPhrases.GetReasonPhrase((int)errorResults[0].Metadata["status"]),
                Detail = $"",
                Instance = $"{httpContext.Request.Path}",
                Type = new Uri($"https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/{(int)errorResults[0].Metadata["status"]}"),
                TraceId = $"{Activity.Current?.Id ?? httpContext.TraceIdentifier}",
                Errors = errors
            };
        }
        #endregion

    }
}
