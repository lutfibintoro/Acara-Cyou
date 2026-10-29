using FluentResults;
using MediatR;

namespace WebServer.Features.TicketSlice.GetTickets
{
    public class GetTicketsQuery : IRequest<Result<List<GetTicketsResponse>>>
    {
        public Guid EventId { get; set; }
    }
}
