using FastEndpoints;

namespace WebServer.Features.GoogleLogin.Redirect
{
    public record RedirectRequest
    (
        [property: FromQuery]
        Query? Query
    );

    public record Query
    (
        string ReturnUrl
    );
}
