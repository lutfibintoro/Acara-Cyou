using System.Text;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.Data.Entities;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.TicketSlice.UpdateTicket
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, Result>
    {
        private readonly ILogger<UpdateTicketCommandHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;

        public UpdateTicketCommandHandler(ILogger<UpdateTicketCommandHandler> logger, AcaraCyouDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            byte[] byteTeks = Convert.FromBase64String(request.ParticipantNotice);
            request.ParticipantNotice = Encoding.UTF8.GetString(byteTeks);

            TicketCategory? ticketCategory = await _dbContext.TicketCategory
                .Where(dbTicket => dbTicket.Id == request.TicketId)
                .FirstOrDefaultAsync(cancellationToken);

            if (ticketCategory is null)
            {
                var notFoundError = new NotFoundError<Guid>("ticketId", request.TicketId, null, Severity.Error);
                return Result.Fail(notFoundError);
            }

            Infrastructures.Data.Entities.Event myEvent = await _dbContext.Event
                .AsNoTracking()
                .Where(dbEvent => dbEvent.Id == ticketCategory.EventId)
                .FirstAsync(cancellationToken);

            if (myEvent.AcaraCyouUserId != request.UserId)
            {
                ForbiddenError<string> forbiddenResultError = new("userId", $"{request.UserId}", null, Severity.Error);
                return Result.Fail(forbiddenResultError);
            }

            for (int attempt = 0; attempt < 10; attempt++)
            {
                ticketCategory.Category = request.Category;
                ticketCategory.ParticipantNotice = request.ParticipantNotice;
                ticketCategory.PriceOne = request.PriceOne;
                ticketCategory.Quota = request.Quota;
                ticketCategory.BookedQuota = request.BookedQuota;
                ticketCategory.IsSalesOpen = request.IsSalesOpen;
                ticketCategory.OpenSalesDate = request.OpenSalesDate;
                ticketCategory.CloseSalesDate = request.CloseSalesDate;
                ticketCategory.IsDeleted = request.IsDeleted;
                ticketCategory.RowVersion++;

                try
                {
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    return Result.Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await Task.Delay(200, cancellationToken);
                    await _dbContext.Entry(ticketCategory).ReloadAsync(cancellationToken);
                }
            }

            var concurrenResultError = new ConcurrencyError<Guid>("ticketId", request.TicketId, $"Ticket [{request.TicketId}] was concurrently modified by another request", Severity.Error);
            return Result.Fail(concurrenResultError);
        }
    }
}