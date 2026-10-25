using FastEndpoints;

namespace WebServer.Features.Event.GetEvents
{
    public class GetEventsSummary : Summary<GetEventsEndpoint>
    {
        public GetEventsSummary()
        {
            Response<List<GetEventsResponse>>(StatusCodes.Status200OK, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
