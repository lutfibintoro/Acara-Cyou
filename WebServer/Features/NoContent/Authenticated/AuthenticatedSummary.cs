using FastEndpoints;

namespace WebServer.Features.NoContent.Authenticated
{
    public class AuthenticatedSummary : Summary<AuthenticatedEndpoint>
    {
        public AuthenticatedSummary()
        {
            Response(StatusCodes.Status204NoContent);
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
        }
    }
}
