namespace WebServer.Features.GoogleLogin.KeyExchange
{
    public record KeyExchangeResponse
    (
        string JsonWebToken,
        string RefreshToken,
        string ProfileImageUrl
    );
}
