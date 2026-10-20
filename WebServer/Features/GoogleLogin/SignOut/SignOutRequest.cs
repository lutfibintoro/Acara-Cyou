using FastEndpoints;

namespace WebServer.Features.GoogleLogin.SignOut
{
    public record SignOutRequest
    (
        [property: FromBody]
        Body? Body
    );

    public record Body
    (
        string RefreshToken
    );
}
