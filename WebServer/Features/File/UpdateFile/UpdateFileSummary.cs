using FastEndpoints;

namespace WebServer.Features.File.UpdateFile
{
    public class UpdateFileSummary : Summary<UpdateFileEndpoint, UpdateFileRequest>
    {
        public UpdateFileSummary()
        {
            Summary = "Update a file with the given [fileName]";
            Description = "Deletes the file with the given [fileName] and stores the uploaded file under the same file name with the uploaded file's extension. Only the user whose id is on the left side of the '+' sign in the file name can update the file.";

            Response<UpdateFileResponse>(StatusCodes.Status200OK, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status403Forbidden, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
