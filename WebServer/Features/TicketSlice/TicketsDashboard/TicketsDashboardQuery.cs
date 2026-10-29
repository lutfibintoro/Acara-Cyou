using FluentResults;
using MediatR;

namespace WebServer.Features.TicketSlice.TicketsDashboard
{
    public class TicketsDashboardQuery : IRequest<Result<List<TicketsDashboardResponse>>>
    {
        public Guid UserId { get; init; }
        public Guid EventId { get; init; }
    }
}
