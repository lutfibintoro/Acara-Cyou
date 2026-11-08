using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.Event.GetEventByLabelPage
{
    public class GetEventByLabelPageQueryHandler : IRequestHandler<GetEventByLabelPageQuery, Result<GetEventByLabelPageResponse>>
    {
        private readonly ILogger<GetEventByLabelPageQueryHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;

        public GetEventByLabelPageQueryHandler(ILogger<GetEventByLabelPageQueryHandler> logger, AcaraCyouDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Result<GetEventByLabelPageResponse>> Handle(GetEventByLabelPageQuery request, CancellationToken cancellationToken)
        {
            var myEvent = await _dbContext.Event
                .AsNoTracking()
                .Where(dbEvent => dbEvent.PageCustomUrl == request.CustomUrlName)
                .Where(dbEvent => dbEvent.IsDeleted == false)
                .Include(dbEvent => dbEvent.Posters)
                .FirstOrDefaultAsync(cancellationToken);

            if (myEvent is null)
            {
                NotFoundError<string> notFoundError = new(nameof(request.CustomUrlName), request.CustomUrlName, null, Severity.Error);
                return Result.Fail<GetEventByLabelPageResponse>(notFoundError);
            }

            return Result.Ok(new GetEventByLabelPageResponse()
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
                Posters = [.. myEvent.Posters.Select(listPoster => new GetEventByLabelPagePoster()
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