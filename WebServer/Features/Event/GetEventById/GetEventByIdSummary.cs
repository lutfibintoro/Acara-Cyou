using FastEndpoints;

namespace WebServer.Features.Event.GetEventById
{
    public class GetEventByIdSummary : Summary<GetEventByIdEndpoint, GetEventByIdRequest>
    {
        public GetEventByIdSummary()
        {
            Summary = "Get an event by route parameter [eventId]";
            Description = "Returns the whole event row including its posters array for the event with the given [eventId]. Returns 404 when the event does not exist and 400 when the event has already been deleted.";

            Params["eventId"] = "The unique identifier (guid) of the event to load";

            Response<GetEventByIdResponse>(StatusCodes.Status200OK, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
