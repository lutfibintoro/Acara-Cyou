using FastEndpoints;

namespace WebServer.Features.GoogleLogin.Challenge
{
    public record ChallengeRequest
    (
        [property: FromQuery]
        Query? Query
    );

    public record Query
    (
        string ReturnUrl
    );
}
