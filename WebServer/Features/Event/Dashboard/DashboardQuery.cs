using FluentResults;
using MediatR;

namespace WebServer.Features.Event.Dashboard
{
    public record DashboardQuery
    (
        Guid UserId,
        Guid EventId
    ) : IRequest<Result<DashboardResponse>>;
}
