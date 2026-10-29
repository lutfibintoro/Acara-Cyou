using FluentResults;
using MediatR;

namespace WebServer.Features.TicketSlice.DeleteTicket
{
    public record DeleteTicketCommand
    (
        Guid TicketId,
        Guid UserId
    ) : IRequest<Result>;
}
