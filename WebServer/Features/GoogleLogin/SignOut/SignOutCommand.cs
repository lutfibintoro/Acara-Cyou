using FluentResults;
using MediatR;

namespace WebServer.Features.GoogleLogin.SignOut
{
    public record SignOutCommand
    (
        string RefreshToken
    ) : IRequest<Result>;
}
