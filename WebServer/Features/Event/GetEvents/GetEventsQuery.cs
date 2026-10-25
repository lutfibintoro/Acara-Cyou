using FluentResults;
using MediatR;

namespace WebServer.Features.Event.GetEvents
{
    public record GetEventsQuery
    (
        Guid UserId
    ) : IRequest<Result<List<GetEventsResponse>>>;
}
