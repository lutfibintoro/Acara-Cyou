using FastEndpoints;

namespace WebServer.Features.GoogleLogin.SignOut
{
    public class SignOutSummary : Summary<SignOutEndpoint, SignOutRequest>
    {
        public SignOutSummary()
        {
            Response(StatusCodes.Status204NoContent);
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
