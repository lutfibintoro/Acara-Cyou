using FastEndpoints;

namespace WebServer.Features.GoogleLogin.RefreshToken
{
    public record RefreshTokenRequest
    (
        [property: FromBody]
        Body Body
    );

    public record Body
    (
        string RefreshToken
    );
}
