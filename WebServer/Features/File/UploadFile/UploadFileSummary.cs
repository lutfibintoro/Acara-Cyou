using FastEndpoints;

namespace WebServer.Features.File.UploadFile
{
    public class UploadFileSummary : Summary<UploadFileEndpoint, UploadFileRequest>
    {
        public UploadFileSummary()
        {
            Response<UploadFileResponse>(StatusCodes.Status201Created, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
