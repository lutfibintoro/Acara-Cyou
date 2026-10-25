using FluentResults;
using MediatR;

namespace WebServer.Features.GoogleLogin.RefreshToken
{
    public record RefreshTokenCommand
    (
        string RefreshToken
    ) : IRequest<Result<RefreshTokenResponse>>;
}
