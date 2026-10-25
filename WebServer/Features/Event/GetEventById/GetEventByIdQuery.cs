using FluentResults;
using MediatR;

namespace WebServer.Features.Event.GetEventById
{
    public record GetEventByIdQuery
    (
        Guid EventId
    ) : IRequest<Result<GetEventByIdResponse>>;
}
