using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Features.TicketSlice.TicketsDashboard;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.Data.Entities;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.TicketSlice.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<GetByIdResponse>>
    {
        private readonly ILogger<GetByIdQueryHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;

        public GetByIdQueryHandler(ILogger<GetByIdQueryHandler> logger, AcaraCyouDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Result<GetByIdResponse>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            TicketCategory? ticket = await _dbContext.TicketCategory
                .AsNoTracking()
                .Where(dbTicket => dbTicket.Id == request.TicketId)
                .FirstOrDefaultAsync(cancellationToken);

            if (ticket is null)
            {
                NotFoundError<string> notFoundResultError = new("ticketId", $"{request.TicketId}", null, Severity.Error);
                return Result.Fail<GetByIdResponse>(notFoundResultError);
            }

            return Result.Ok(new GetByIdResponse()
            {
                TicketId = ticket.Id,
                Category = ticket.Category,
                BookedQuota = ticket.BookedQuota,
                CloseSalesDate = ticket.CloseSalesDate,
                IsSalesOpen = ticket.IsSalesOpen,
                OpenSalesDate = ticket.OpenSalesDate,
                ParticipantNotice = ticket.ParticipantNotice,
                PriceOne = ticket.PriceOne,
                Quota = ticket.Quota
            });
        }
    }
}