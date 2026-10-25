using FluentResults;
using MediatR;

namespace WebServer.Features.Event.AddEvent
{
    public record AddEventCommand
    (
        Guid UserId,
        string Title,
        string Description,
        List<Poster>? Posters
    ) : IRequest<Result>;
}
