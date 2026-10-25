using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.Event.UpdateEventById
{
    public class UpdateEventByIdCommandHandler : IRequestHandler<UpdateEventByIdCommand, Result>
    {
        private const int ConcurrencyRetryLimit = 10;
        private static readonly TimeSpan ConcurrencyRetryDelay = TimeSpan.FromMilliseconds(200);

        private readonly ILogger<UpdateEventByIdCommandHandler> _logger;
        private readonly AcaraCyouDbContext _acaraCyouDbContext;

        public UpdateEventByIdCommandHandler(ILogger<UpdateEventByIdCommandHandler> logger, AcaraCyouDbContext acaraCyouDbContext)
        {
            _logger = logger;
            _acaraCyouDbContext = acaraCyouDbContext;
        }

        public async Task<Result> Handle(UpdateEventByIdCommand request, CancellationToken cancellationToken)
        {
            Infrastructures.Data.Entities.Event? myEvent = await _acaraCyouDbContext.Event
                .Where(dbEvent =>
                    dbEvent.Id == request.EventId &&
                    dbEvent.AcaraCyouUserId == request.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (myEvent is null)
            {
                NotFoundError<string> notFoundError = new("eventId", $"{request.EventId}", null, Severity.Error);
                return Result.Fail(notFoundError);
            }

            for (int attempt = 1; attempt <= ConcurrencyRetryLimit; attempt++)
            {
                myEvent.Title = request.Title;
                myEvent.Description = request.Description;
                myEvent.FormTemplate = request.FormTemplate;
                myEvent.IsRegistrationOpen = request.IsRegistrationOpen;
                myEvent.OpenRegistrationDate = request.OpenRegistrationDate;
                myEvent.CloseRegistrationDate = request.CloseRegistrationDate;
                myEvent.BankAccountNumber = request.BankAccountNumber;
                myEvent.BankAccountName = request.BankAccountName;
                myEvent.IsDeleted = request.IsDeleted;
                myEvent.RowVersion++;

                try
                {
                    await _acaraCyouDbContext.SaveChangesAsync(cancellationToken);

                    _logger.LogInformation("Event [{EventId}] was updated to RowVersion [{RowVersion}] by user [{UserId}] after [{Attempt}] attempt(s)", request.EventId, myEvent.RowVersion, request.UserId, attempt);

                    return Result.Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await Task.Delay(ConcurrencyRetryDelay, cancellationToken);
                    await _acaraCyouDbContext.Entry(myEvent).ReloadAsync(cancellationToken);
                }
            }

            ConcurrencyError<string> resultError = new("eventId", $"{request.EventId}", $"Event [{request.EventId}] was concurrently modified by another request", Severity.Error);
            return Result.Fail(resultError);
        }
    }
}
