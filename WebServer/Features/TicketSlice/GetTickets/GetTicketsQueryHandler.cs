using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.TicketSlice.GetTickets
{
    public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, Result<List<GetTicketsResponse>>>
    {
        private readonly ILogger<GetTicketsQueryHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;

        public GetTicketsQueryHandler(ILogger<GetTicketsQueryHandler> logger, AcaraCyouDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Result<List<GetTicketsResponse>>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            Infrastructures.Data.Entities.Event? myEvent = await _dbContext.Event
                .AsNoTracking()
                .Where(dbEvent => dbEvent.Id == request.EventId)
                .Where(dbEvent => dbEvent.IsDeleted == false)
                .Where(dbEvent => dbEvent.IsRegistrationOpen == true)
                .Include(dbEvent => dbEvent.TicketCategorys)
                .FirstOrDefaultAsync(cancellationToken);

            if (myEvent is null)
            {
                var eventNotFound = new NotFoundError<Guid>("eventId", request.EventId, null, FluentValidation.Severity.Error);
                return Result.Fail<List<GetTicketsResponse>>(eventNotFound);
            }

            List<GetTicketsResponse> tickets = myEvent.TicketCategorys
                .Where(ticketCategory => ticketCategory.IsDeleted == false)
                .Select(ticketCategory => new GetTicketsResponse()
                {
                    TicketId = ticketCategory.Id,
                    Category = ticketCategory.Category,
                    Quota = ticketCategory.Quota,
                    BookedQuota = ticketCategory.BookedQuota,
                    PriceOne = ticketCategory.PriceOne,
                    IsSalesOpen = ticketCategory.IsSalesOpen,
                    OpenSalesDate = ticketCategory.OpenSalesDate,
                    CloseSalesDate = ticketCategory.CloseSalesDate
                })
                .ToList();

            return Result.Ok(tickets);
        }
    }
}
