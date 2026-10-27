using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.Poster.UpdatePosterById
{
    public class UpdatePosterByIdCommandHandler : IRequestHandler<UpdatePosterByIdCommand, Result>
    {
        private readonly ILogger<UpdatePosterByIdCommandHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;

        public UpdatePosterByIdCommandHandler(ILogger<UpdatePosterByIdCommandHandler> logger, AcaraCyouDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(UpdatePosterByIdCommand request, CancellationToken cancellationToken)
        {
            Infrastructures.Data.Entities.Poster? poster = await _dbContext.Poster
                .Where(dbPoster => dbPoster.Id == request.PosterId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (poster is null)
            {
                var posterNotFound = new NotFoundError<string>("posterId", $"{request.PosterId}", null, Severity.Error);
                return Result.Fail(posterNotFound);
            }

            Infrastructures.Data.Entities.Event? myEvent = await _dbContext.Event
                .Where(dbEvent => dbEvent.Id == poster.EventId)
                .FirstOrDefaultAsync(cancellationToken);

            if (myEvent is null)
            {
                var eventNotFound = new NotFoundError<string>("eventId", $"{poster.EventId}", null, Severity.Error);
                return Result.Fail(eventNotFound);
            }

            if ($"{myEvent.AcaraCyouUserId}" != $"{request.UserId}")
            {
                var posterForbiden = new ForbiddenError<string>("user", $"{request.UserId}", null, Severity.Error);
                return Result.Fail(posterForbiden);
            }

            for (int attempt = 0; attempt < 10; attempt++)
            {
                poster.FullPathUrl = request.FullPathUrl;
                poster.MimeType = request.MimeType;
                poster.ByteLength = request.ByteLength;
                poster.RowVersion++;

                try
                {
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    return Result.Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await Task.Delay(200, cancellationToken);
                    poster = await _dbContext.Poster
                        .Where(dbPoster => dbPoster.Id == request.PosterId)
                        .FirstAsync(cancellationToken);
                }
            }

            ConcurrencyError<string> concurrenResultError = new("eventId", $"{request.PosterId}", $"Event [{request.PosterId}] was concurrently modified by another request", Severity.Error);
            return Result.Fail(concurrenResultError);
        }
    }
}
