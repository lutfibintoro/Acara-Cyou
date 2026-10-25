using FastEndpoints;

namespace WebServer.Features.Event.UpdateEventById
{
    public class UpdateEventByIdSummary : Summary<UpdateEventByIdEndpoint, UpdateEventByIdRequest>
    {
        public UpdateEventByIdSummary()
        {
            Summary = "Update an event by route parameter [eventId]";
            Description = "Updates all columns of the event with the given [eventId]. Only the user related (owner) to the event can update it. The body also accepts isDeleted, so an already soft deleted event can be restored with isDeleted=false. Returns 204 NoContent on success.";

            Params["eventId"] = "The unique identifier (guid) of the event to update";

            Response(StatusCodes.Status204NoContent);
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status409Conflict, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
