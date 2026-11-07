using FastEndpoints;

namespace WebServer.Features.CustomUrl.GetCustomUrl
{
    public class GetCustomUrlSummary : Summary<GetCustomUrlEndpoint>
    {
        public GetCustomUrlSummary()
        {
            Response<List<GetCustomUrlResponse>>(StatusCodes.Status200OK, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
