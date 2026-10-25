using FluentResults;
using MediatR;

namespace WebServer.Features.Event.DeleteEventById
{
    public record DeleteEventByIdCommand
    (
        Guid UserId,
        Guid EventId
    ) : IRequest<Result>;
}
