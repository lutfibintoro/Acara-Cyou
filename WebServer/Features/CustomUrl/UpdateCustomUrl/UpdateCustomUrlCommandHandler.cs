using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.Data.Entities;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.CustomUrl.UpdateCustomUrl
{
    public class UpdateCustomUrlCommandHandler : IRequestHandler<UpdateCustomUrlCommand, Result<UpdateCustomUrlResponse>>
    {
        private readonly ILogger<UpdateCustomUrlCommandHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;

        public UpdateCustomUrlCommandHandler(ILogger<UpdateCustomUrlCommandHandler> logger, AcaraCyouDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Result<UpdateCustomUrlResponse>> Handle(UpdateCustomUrlCommand request, CancellationToken cancellationToken)
        {
            Infrastructures.Data.Entities.Event? myEnvet = await _dbContext.Event
                .Where(dbEvent => dbEvent.Id == request.EventId)
                .FirstOrDefaultAsync(cancellationToken);

            if (myEnvet is null)
            {
                NotFoundError<string> notFoundError = new("eventId", request.EventId.ToString(), null, Severity.Error);
                return Result.Fail<UpdateCustomUrlResponse>(notFoundError);
            }

            if (myEnvet.AcaraCyouUserId != request.UserId)
            {
                ForbiddenError<string> forbiddenError = new("userId", request.UserId.ToString(), null, Severity.Error);
                return Result.Fail<UpdateCustomUrlResponse>(forbiddenError);
            }

            Infrastructures.Data.Entities.Event[] duplicateCustomUrlEvent = await _dbContext.Event
                .AsNoTracking()
                .Where(dbEvent => dbEvent.Id != request.EventId)
                .Where(dbEvent => dbEvent.FormCustomUrl == request.FormCustomUrl || dbEvent.PageCustomUrl == request.PageCustomUrl)
                .Select(dbEvent => new Infrastructures.Data.Entities.Event()
                {
                    FormCustomUrl = dbEvent.FormCustomUrl,
                    PageCustomUrl = dbEvent.PageCustomUrl
                })
                .ToArrayAsync(cancellationToken);

            if (duplicateCustomUrlEvent.Length > 0)
            {
                bool duplicateFormCustomUrl = duplicateCustomUrlEvent.Any(duplicateCustomUrl => duplicateCustomUrl.FormCustomUrl == request.FormCustomUrl);
                bool duplicatePageCustomUrl = duplicateCustomUrlEvent.Any(duplicateCustomUrl => duplicateCustomUrl.PageCustomUrl == request.PageCustomUrl);

                if (duplicateFormCustomUrl)
                {
                    DuplicateError<string> duplicateError = new("formCustomUrl", request.FormCustomUrl, null, Severity.Error);
                    return Result.Fail<UpdateCustomUrlResponse>(duplicateError);
                }
                if (duplicatePageCustomUrl)
                {
                    DuplicateError<string> duplicateError = new("pageCustomUrl", request.PageCustomUrl, null, Severity.Error);
                    return Result.Fail<UpdateCustomUrlResponse>(duplicateError);
                }
            }

            for (int attempt = 0; attempt < 10; attempt++)
            {
                myEnvet.FormCustomUrl = request.FormCustomUrl;
                myEnvet.PageCustomUrl = request.PageCustomUrl;
                myEnvet.RowVersion++;

                try
                {
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    return Result.Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await Task.Delay(200, cancellationToken);
                    await _dbContext.Entry(myEnvet).ReloadAsync(cancellationToken);
                }
            }

            var concurrenResultError = new ConcurrencyError<Guid>("event", request.EventId, $"event [{request.EventId}] was concurrently modified by another request", Severity.Error);
            return Result.Fail(concurrenResultError);
        }
    }
}
