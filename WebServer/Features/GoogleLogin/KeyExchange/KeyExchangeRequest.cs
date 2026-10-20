using FastEndpoints;

namespace WebServer.Features.GoogleLogin.KeyExchange
{
    public record KeyExchangeRequest
    (
        [property: FromQuery]
        Query? Query
    );

    public record Query
    (
        string Key
    );
}
