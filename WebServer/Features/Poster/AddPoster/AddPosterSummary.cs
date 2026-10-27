using FastEndpoints;

namespace WebServer.Features.Poster.AddPoster
{
    public class AddPosterSummary : Summary<AddPosterEndpoint, AddPosterRequest>
    {
        public AddPosterSummary()
        {
            Description = "Adds a new poster to the event with the [eventId] given in the body. Only the owner of the event can add posters: returns 403 when the caller is not the owner, 404 when the event does not exist, and 400 when the event has been deleted or already has the maximum of 5 posters.";

            Response<AddPosterResponse>(StatusCodes.Status200OK, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status403Forbidden, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
