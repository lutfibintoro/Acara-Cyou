using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.Event.GetEventById
{
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Result<GetEventByIdResponse>>
    {
        private readonly ILogger<GetEventByIdQueryHandler> _logger;
        private readonly AcaraCyouDbContext _acaraCyouDbContext;

        public GetEventByIdQueryHandler(ILogger<GetEventByIdQueryHandler> logger, AcaraCyouDbContext acaraCyouDbContext)
        {
            _logger = logger;
            _acaraCyouDbContext = acaraCyouDbContext;
        }

        public async Task<Result<GetEventByIdResponse>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            GetEventByIdResponse? myEvent = await _acaraCyouDbContext.Event
                .AsNoTracking()
                .Where(dbEvent => dbEvent.Id == request.EventId)
                .Select(dbEvent => new GetEventByIdResponse()
                {
                    EventId = dbEvent.Id,
                    AcaraCyouUserId = dbEvent.AcaraCyouUserId,
                    Title = dbEvent.Title,
                    Description = dbEvent.Description,
                    FormTemplate = dbEvent.FormTemplate,
                    IsRegistrationOpen = dbEvent.IsRegistrationOpen,
                    OpenRegistrationDate = dbEvent.OpenRegistrationDate,
                    CloseRegistrationDate = dbEvent.CloseRegistrationDate,
                    BankAccountNumber = dbEvent.BankAccountNumber,
                    BankAccountName = dbEvent.BankAccountName,
                    IsDeleted = dbEvent.IsDeleted,
                    CreatedAt = dbEvent.CreatedAt,
                    RowVersion = dbEvent.RowVersion,
                    Posters = dbEvent.Posters
                        .Select(poster => new Poster()
                        {
                            PosterId = poster.Id,
                            FullPathUrl = poster.FullPathUrl,
                            MimeType = poster.MimeType,
                            ByteLength = poster.ByteLength,
                            CreatedAt = poster.CreatedAt,
                            RowVersion = poster.RowVersion,
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (myEvent is null)
            {
                NotFoundError<string> resultError = new("eventId", $"{request.EventId}", null, FluentValidation.Severity.Error);
                return Result.Fail(resultError);
            }

            if (myEvent.IsDeleted)
            {
                ValidationError<string> resultError = new("eventId", $"{request.EventId}", $"Event [{request.EventId}] has been deleted and cannot be shown", FluentValidation.Severity.Error);
                return Result.Fail(resultError);
            }

            _logger.LogInformation("Event [{EventId}] was loaded", request.EventId);

            return Result.Ok(myEvent);
        }
    }
}
