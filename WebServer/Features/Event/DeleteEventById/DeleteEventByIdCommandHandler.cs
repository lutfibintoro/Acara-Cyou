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
                NotFoundError<string> resultError = new("eventId", $"{request.EventId}", null, FluentValidation.Severity.Error);
                return Result.Fail(resultError);
            }

            myEvent.IsDeleted = true;
            await _acaraCyouDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Event [{EventId}] was soft deleted by user [{UserId}]", request.EventId, request.UserId);

            return Result.Ok();
        }
    }
}
