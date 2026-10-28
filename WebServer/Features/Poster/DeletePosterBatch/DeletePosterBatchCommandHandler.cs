using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.Poster.DeletePosterBatch
{
    public class DeletePosterBatchCommandHandler : IRequestHandler<DeletePosterBatchCommand, Result>
    {
        private readonly ILogger<DeletePosterBatchCommandHandler> _logger;
        private readonly AcaraCyouDbContext _acaraCyouDbContext;

        public DeletePosterBatchCommandHandler(ILogger<DeletePosterBatchCommandHandler> logger, AcaraCyouDbContext acaraCyouDbContext)
        {
            _logger = logger;
            _acaraCyouDbContext = acaraCyouDbContext;
        }

        public async Task<Result> Handle(DeletePosterBatchCommand request, CancellationToken cancellationToken)
        {
            List<Guid> dedupedPosterIds = request.PosterIds.Distinct().ToList();

            List<Guid> existingPosterOwnerIds = await _acaraCyouDbContext.Poster
                .AsNoTracking()
                .Where(dbPoster => dedupedPosterIds.Contains(dbPoster.Id))
                .Select(dbPoster => dbPoster.Event.AcaraCyouUserId)
                .ToListAsync(cancellationToken);

            if (existingPosterOwnerIds.Any(ownerId => ownerId != request.UserId))
            {
                ForbiddenError<string> resultError = new("posterIds", $"{string.Join(", ", dedupedPosterIds)}", null, Severity.Error);
                return Result.Fail(resultError);
            }

            int deletedCount = await _acaraCyouDbContext.Poster
                .Where(dbPoster => dedupedPosterIds.Contains(dbPoster.Id))
                .ExecuteDeleteAsync(cancellationToken);

            _logger.LogInformation("Poster batch [{PosterIds}] was deleted ({DeletedCount}/{RequestedCount}) by user [{UserId}]", string.Join(", ", dedupedPosterIds), deletedCount, dedupedPosterIds.Count, request.UserId);

            return Result.Ok();
        }
    }
}
