using FastEndpoints;

namespace WebServer.Features.GoogleLogin.KeyExchange
{
    public class KeyExchangeSummary : Summary<KeyExchangeEndpoint, KeyExchangeRequest>
    {
        public KeyExchangeSummary()
        {
            Response<KeyExchangeResponse>(StatusCodes.Status200OK, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
