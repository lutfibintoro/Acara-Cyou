using FastEndpoints;

namespace WebServer.Features.GoogleLogin.SignOut
{
    public record SignOutRequest
    (
        [property: FromQuery]
        Query? Query
    );

    public record Query
    (
        string RefreshToken
    );
}
