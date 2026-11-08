using FastEndpoints;

namespace WebServer.Features.Event.GetEventByLabelPage
{
    public class GetEventByLabelPageSummary : Summary<GetEventByLabelPageEndpoint, GetEventByLabelPageRequest>
    {
        public GetEventByLabelPageSummary()
        {
            Response<GetEventByLabelPageResponse>(StatusCodes.Status200OK, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
