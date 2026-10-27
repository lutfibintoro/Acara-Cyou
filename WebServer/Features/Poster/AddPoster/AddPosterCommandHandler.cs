using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.Poster.AddPoster
{
    public class AddPosterCommandHandler : IRequestHandler<AddPosterCommand, Result<AddPosterResponse>>
    {
        private const int MaxPostersPerEvent = 5;
        private const int ConcurrencyRetryLimit = 10;
        private static readonly TimeSpan ConcurrencyRetryDelay = TimeSpan.FromMilliseconds(200);

        private readonly ILogger<AddPosterCommandHandler> _logger;
        private readonly AcaraCyouDbContext _acaraCyouDbContext;

        public AddPosterCommandHandler(ILogger<AddPosterCommandHandler> logger, AcaraCyouDbContext acaraCyouDbContext)
        {
            _logger = logger;
            _acaraCyouDbContext = acaraCyouDbContext;
        }

        public async Task<Result<AddPosterResponse>> Handle(AddPosterCommand request, CancellationToken cancellationToken)
        {
            Infrastructures.Data.Entities.Event? myEvent = await _acaraCyouDbContext.Event
                .Where(dbEvent => dbEvent.Id == request.EventId)
                .FirstOrDefaultAsync(cancellationToken);

            if (myEvent is null)
            {
                NotFoundError<string> resultError = new("eventId", $"{request.EventId}", null, Severity.Error);
                return Result.Fail(resultError);
            }

            if (myEvent.AcaraCyouUserId != request.UserId)
            {
                ForbiddenError<string> resultError = new("eventId", $"{request.EventId}", null, Severity.Error);
                return Result.Fail(resultError);
            }

            if (myEvent.IsDeleted)
            {
                ValidationError<string> resultError = new("eventId", $"{request.EventId}", $"Event [{request.EventId}] has been deleted and cannot be modified", Severity.Error);
                return Result.Fail(resultError);
            }

            for (int attempt = 1; attempt <= ConcurrencyRetryLimit; attempt++)
            {
                int posterCount = await _acaraCyouDbContext.Poster
                    .CountAsync(poster => poster.EventId == request.EventId, cancellationToken);

                if (posterCount >= MaxPostersPerEvent)
                {
                    ValidationError<string> resultError = new("eventId", $"{request.EventId}", $"Event [{request.EventId}] already has the maximum of {MaxPostersPerEvent} posters", Severity.Error);
                    return Result.Fail(resultError);
                }

                Infrastructures.Data.Entities.Poster newPoster = new()
                {
                    Id = Guid.NewGuid(),
                    EventId = request.EventId,
                    FullPathUrl = request.FullPathUrl,
                    MimeType = request.MimeType,
                    ByteLength = request.ByteLength
                };

                _acaraCyouDbContext.Poster.Add(newPoster);
                myEvent.RowVersion++;

                try
                {
                    await _acaraCyouDbContext.SaveChangesAsync(cancellationToken);

                    _logger.LogInformation("Poster [{PosterId}] was added to event [{EventId}] by user [{UserId}] after [{Attempt}] attempt(s)", newPoster.Id, request.EventId, request.UserId, attempt);

                    return Result.Ok(new AddPosterResponse(
                        PosterId: newPoster.Id,
                        FullPathUrl: newPoster.FullPathUrl,
                        MimeType: newPoster.MimeType,
                        ByteLength: newPoster.ByteLength));
                }
                catch (DbUpdateConcurrencyException)
                {
                    await Task.Delay(ConcurrencyRetryDelay, cancellationToken);
                    _acaraCyouDbContext.Entry(newPoster).State = EntityState.Detached;
                    await _acaraCyouDbContext.Entry(myEvent).ReloadAsync(cancellationToken);
                }
            }

            ConcurrencyError<string> concurentResultError = new("eventId", $"{request.EventId}", $"Event [{request.EventId}] was concurrently modified by another request", Severity.Error);
            return Result.Fail(concurentResultError);
        }
    }
}
