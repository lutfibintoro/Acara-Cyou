using FastEndpoints;

namespace WebServer.Features.GoogleLogin.RefreshToken
{
    public class RefreshTokenSummary : Summary<RefreshTokenEndpoint, RefreshTokenRequest>
    {
        public RefreshTokenSummary()
        {
            Response<RefreshTokenResponse>(StatusCodes.Status200OK, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
