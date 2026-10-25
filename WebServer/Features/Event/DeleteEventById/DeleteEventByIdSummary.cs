using FastEndpoints;

namespace WebServer.Features.Event.DeleteEventById
{
    public class DeleteEventByIdSummary : Summary<DeleteEventByIdEndpoint, DeleteEventByIdRequest>
    {
        public DeleteEventByIdSummary()
        {
            Summary = "Soft delete an event by query string parameter [eventId]";
            Description = "Soft deletes the event with the given [eventId] by setting its isDeleted column to true. The event row is not permanently removed from the database. Only events owned by the currently authenticated user and not already deleted can be soft deleted.";

            Params["eventId"] = "The unique identifier (guid) of the event to soft delete";

            Response(StatusCodes.Status204NoContent);
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status409Conflict, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
