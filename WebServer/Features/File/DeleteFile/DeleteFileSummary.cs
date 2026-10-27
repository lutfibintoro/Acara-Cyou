using FastEndpoints;

namespace WebServer.Features.File.DeleteFile
{
    public class DeleteFileSummary : Summary<DeleteFileEndpoint, DeleteFileRequest>
    {
        public DeleteFileSummary()
        {
            Description = "Deletes the file with the given [fileName]. Only the user whose id is on the left side of the '+' sign in the file name can delete the file. Returns 204 whether the file was deleted or it does not exist.";

            Response(StatusCodes.Status204NoContent);
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status403Forbidden, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
