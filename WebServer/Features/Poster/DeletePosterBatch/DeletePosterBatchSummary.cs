using FastEndpoints;

namespace WebServer.Features.Poster.DeletePosterBatch
{
    public class DeletePosterBatchSummary : Summary<DeletePosterBatchEndpoint, DeletePosterBatchRequest>
    {
        public DeletePosterBatchSummary()
        {
            Description = "Deletes the posters with the given [posterIds] query values. Only the owner of the events the posters belong to can delete them: returns 403 when any of the requested posters belongs to another user's event, and deletes nothing in that case. Posters that do not exist are skipped. Returns 204 regardless of how many posters were deleted.";

            Response(StatusCodes.Status204NoContent);
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status403Forbidden, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
