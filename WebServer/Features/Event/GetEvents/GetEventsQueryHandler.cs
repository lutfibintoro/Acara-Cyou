using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;

namespace WebServer.Features.Event.GetEvents
{
    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, Result<List<GetEventsResponse>>>
    {
        private readonly ILogger<GetEventsQueryHandler> _logger;
        private readonly AcaraCyouDbContext _acaraCyouDbContext;

        public GetEventsQueryHandler(ILogger<GetEventsQueryHandler> logger, AcaraCyouDbContext acaraCyouDbContext)
        {
            _logger = logger;
            _acaraCyouDbContext = acaraCyouDbContext;
        }

        public async Task<Result<List<GetEventsResponse>>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            List<GetEventsResponse> events = await _acaraCyouDbContext.Event
                .AsNoTracking()
                .Where(myEvent => myEvent.AcaraCyouUserId == request.UserId)
                .Where(myEvent => myEvent.IsDeleted == false)
                .Select(myEvent => new GetEventsResponse()
                {
                    EventId = myEvent.Id,
                    Title = myEvent.Title,
                    Description = myEvent.Description,
                    Posters = myEvent.Posters
                        .Select(poster => new Poster()
                        {
                            FullPathUrl = poster.FullPathUrl,
                            MimeType = poster.MimeType,
                            ByteLength = poster.ByteLength
                        })
                        .ToList()
                })
                .ToListAsync(cancellationToken);


            return Result.Ok<List<GetEventsResponse>>(events);
        }
    }
}
