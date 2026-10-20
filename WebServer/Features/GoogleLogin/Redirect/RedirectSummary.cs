using FastEndpoints;

namespace WebServer.Features.GoogleLogin.Redirect
{
    public class RedirectSummary : Summary<RedirectEndpoint, RedirectRequest>
    {
        public RedirectSummary()
        {
            Response(StatusCodes.Status204NoContent);
            Response(StatusCodes.Status302Found);
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
