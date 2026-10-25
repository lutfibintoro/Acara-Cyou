using FastEndpoints;

namespace WebServer.Features.Event.AddEvent
{
    public class AddEventSummary : Summary<AddEventEndpoint, AddEventRequest>
    {
        public AddEventSummary()
        {
            Response(StatusCodes.Status204NoContent);
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
