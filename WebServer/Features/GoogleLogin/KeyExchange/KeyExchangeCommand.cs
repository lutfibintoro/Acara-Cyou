using FluentResults;
using MediatR;

namespace WebServer.Features.GoogleLogin.KeyExchange
{
    public record KeyExchangeCommand
    (
        string Key
    ) : IRequest<Result<KeyExchangeResponse>>;
}
