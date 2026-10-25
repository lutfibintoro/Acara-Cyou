using FastEndpoints;

namespace WebServer.Features.File.GetFile
{
    public class GetFileSummary : Summary<GetFileEndpoint, GetFileRequest>
    {
        public GetFileSummary()
        {
            Response(StatusCodes.Status204NoContent);
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
