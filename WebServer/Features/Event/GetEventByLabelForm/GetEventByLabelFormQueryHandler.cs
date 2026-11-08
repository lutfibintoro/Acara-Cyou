using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.Event.GetEventByLabelForm
{
    public class GetEventByLabelFormQueryHandler : IRequestHandler<GetEventByLabelFormQuery, Result<GetEventByLabelFormResponse>>
    {
        private readonly ILogger<GetEventByLabelFormQueryHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;

        public GetEventByLabelFormQueryHandler(ILogger<GetEventByLabelFormQueryHandler> logger, AcaraCyouDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Result<GetEventByLabelFormResponse>> Handle(GetEventByLabelFormQuery request, CancellationToken cancellationToken)
        {
            var myEvent = await _dbContext.Event
                .AsNoTracking()
                .Where(dbEvent => dbEvent.FormCustomUrl == request.CustomUrlName)
                .Where(dbEvent => dbEvent.IsDeleted == false)
                .Include(dbEvent => dbEvent.Posters)
                .FirstOrDefaultAsync(cancellationToken);

            if (myEvent is null)
            {
                NotFoundError<string> notFoundError = new(nameof(request.CustomUrlName), request.CustomUrlName, null, Severity.Error);
                return Result.Fail<GetEventByLabelFormResponse>(notFoundError);
            }

            return Result.Ok(new GetEventByLabelFormResponse()
            {
                EventId = myEvent.Id,
                Title = myEvent.Title,
                Description = myEvent.Description,
                AfterBuyDescription = myEvent.AfterBuyDescription,
                FormTemplate = myEvent.FormTemplate,
                FormCustomUrl = myEvent.FormCustomUrl,
                PageCustomUrl = myEvent.PageCustomUrl,
                IsRegistrationOpen = myEvent.IsRegistrationOpen,
                OpenRegistrationDate = myEvent.OpenRegistrationDate,
                CloseRegistrationDate = myEvent.CloseRegistrationDate,
                BankAccountNumber = myEvent.BankAccountNumber,
                BankAccountName = myEvent.BankAccountName,
                IsDeleted = myEvent.IsDeleted,
                Posters = [.. myEvent.Posters.Select(listPoster => new GetEventByLabelFormPoster()
                {
                    PosterId = listPoster.Id,
                    FullPathUrl = listPoster.FullPathUrl,
                    MimeType = listPoster.MimeType,
                    ByteLength = listPoster.ByteLength
                })]
            });
        }
    }
}