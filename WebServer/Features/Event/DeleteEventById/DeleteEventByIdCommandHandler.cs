using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.Event.DeleteEventById
{
    public class DeleteEventByIdCommandHandler : IRequestHandler<DeleteEventByIdCommand, Result>
    {
        private const int ConcurrencyRetryLimit = 10;
        private static readonly TimeSpan ConcurrencyRetryDelay = TimeSpan.FromMilliseconds(200);

        private readonly ILogger<DeleteEventByIdCommandHandler> _logger;
        private readonly AcaraCyouDbContext _acaraCyouDbContext;

        public DeleteEventByIdCommandHandler(ILogger<DeleteEventByIdCommandHandler> logger, AcaraCyouDbContext acaraCyouDbContext)
        {
            _logger = logger;
            _acaraCyouDbContext = acaraCyouDbContext;
        }

        public async Task<Result> Handle(DeleteEventByIdCommand request, CancellationToken cancellationToken)
        {
            Infrastructures.Data.Entities.Event? myEvent = await _acaraCyouDbContext.Event
                .Where(dbEvent =>
                    dbEvent.Id == request.EventId &&
                    dbEvent.AcaraCyouUserId == request.UserId &&
                    !dbEvent.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            if (myEvent is null)
            {
                NotFoundError<string> notFoundError = new("eventId", $"{request.EventId}", null, FluentValidation.Severity.Error);
                return Result.Fail(notFoundError);
            }

            for (int attempt = 1; attempt <= ConcurrencyRetryLimit; attempt++)
            {
                myEvent.IsDeleted = true;
                myEvent.RowVersion++;

                try
                {
                    await _acaraCyouDbContext.SaveChangesAsync(cancellationToken);

                    _logger.LogInformation("Event [{EventId}] was soft deleted at RowVersion [{RowVersion}] by user [{UserId}] after [{Attempt}] attempt(s)", request.EventId, myEvent.RowVersion, request.UserId, attempt);

                    return Result.Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await Task.Delay(ConcurrencyRetryDelay, cancellationToken);
                    await _acaraCyouDbContext.Entry(myEvent).ReloadAsync(cancellationToken);
                }
            }

            ConcurrencyError<string> resultError = new("eventId", $"{request.EventId}", $"Event [{request.EventId}] was concurrently modified by another request", FluentValidation.Severity.Error);
            return Result.Fail(resultError);
        }
    }
}
