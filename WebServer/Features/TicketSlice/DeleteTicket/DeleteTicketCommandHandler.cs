using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.Data.Entities;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.TicketSlice.DeleteTicket
{
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, Result>
    {
        private const int ConcurrencyRetryLimit = 10;
        private static readonly TimeSpan ConcurrencyRetryDelay = TimeSpan.FromMilliseconds(200);

        private readonly ILogger<DeleteTicketCommandHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;

        public DeleteTicketCommandHandler(ILogger<DeleteTicketCommandHandler> logger, AcaraCyouDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
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

            for (int attempt = 1; attempt <= ConcurrencyRetryLimit; attempt++)
            {
                ticketCategory.IsDeleted = true;
                ticketCategory.RowVersion++;

                try
                {
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation("Ticket [{TicketId}] was soft deleted at RowVersion [{RowVersion}] by user [{UserId}] after [{Attempt}] attempt(s)", request.TicketId, ticketCategory.RowVersion, request.UserId, attempt);
                    return Result.Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await Task.Delay(ConcurrencyRetryDelay, cancellationToken);
                    await _dbContext.Entry(ticketCategory).ReloadAsync(cancellationToken);
                }
            }

            var concurrenResultError = new ConcurrencyError<Guid>("ticketId", request.TicketId, $"Ticket [{request.TicketId}] was concurrently modified by another request", Severity.Error);
            return Result.Fail(concurrenResultError);
        }
    }
}
