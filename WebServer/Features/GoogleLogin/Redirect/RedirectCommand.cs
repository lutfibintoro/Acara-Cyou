using FluentResults;
using MediatR;

namespace WebServer.Features.GoogleLogin.Redirect
{
    public record RedirectCommand
    (
        string NameIdentifier,
        string Name,
        string Email,
        string LoginProvider,
        string ProfileImageUrl
    ) : IRequest<Result<string>>;
}
