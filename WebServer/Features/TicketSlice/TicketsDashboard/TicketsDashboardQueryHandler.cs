using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.TicketSlice.TicketsDashboard
{
    public class TicketsDashboardQueryHandler : IRequestHandler<TicketsDashboardQuery, Result<List<TicketsDashboardResponse>>>
    {
        private readonly ILogger<TicketsDashboardQueryHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;

        public TicketsDashboardQueryHandler(ILogger<TicketsDashboardQueryHandler> logger, AcaraCyouDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Result<List<TicketsDashboardResponse>>> Handle(TicketsDashboardQuery request, CancellationToken cancellationToken)
        {
            Infrastructures.Data.Entities.Event? myEvent = await _dbContext.Event
                .AsNoTracking()
                .Where(dbEvent => dbEvent.Id == request.EventId)
                .Select(dbEvent => new Infrastructures.Data.Entities.Event()
                {
                    Id = dbEvent.Id,
                    AcaraCyouUserId = dbEvent.AcaraCyouUserId
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (myEvent is null)
            {
                NotFoundError<string> notFoundResultError = new("eventId", $"{request.EventId}", null, Severity.Error);
                return Result.Fail<List<TicketsDashboardResponse>>(notFoundResultError);
            }

            if (myEvent.AcaraCyouUserId != request.UserId)
            {
                ForbiddenError<string> forbiddenResultError = new("userId", $"{request.UserId}", null, Severity.Error);
                return Result.Fail<List<TicketsDashboardResponse>>(forbiddenResultError);
            }

            List<TicketsDashboardResponse> tickets = await _dbContext.TicketCategory
                .AsNoTracking()
                .Where(dbTicket => dbTicket.EventId == myEvent.Id)
                .Select(dbTicket => new TicketsDashboardResponse()
                {
                    TicketId = dbTicket.Id,
                    EventId = dbTicket.EventId,
                    Category = dbTicket.Category,
                    ParticipantNotice = dbTicket.ParticipantNotice,
                    PriceOne = dbTicket.PriceOne,
                    Quota = dbTicket.Quota,
                    BookedQuota = dbTicket.BookedQuota,
                    CloseSalesDate = dbTicket.CloseSalesDate,
                    IsDeleted = dbTicket.IsDeleted,
                    IsSalesOpen = dbTicket.IsSalesOpen,
                    OpenSalesDate = dbTicket.OpenSalesDate
                })
                .ToListAsync(cancellationToken);

            return Result.Ok(tickets);
        }
    }
}