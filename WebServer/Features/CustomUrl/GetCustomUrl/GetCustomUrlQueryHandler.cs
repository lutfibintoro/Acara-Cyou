using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;

namespace WebServer.Features.CustomUrl.GetCustomUrl
{
    public class GetCustomUrlQueryHandler : IRequestHandler<GetCustomUrlQuery, Result<List<GetCustomUrlResponse>>>
    {
        private readonly ILogger<GetCustomUrlQueryHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;

        public GetCustomUrlQueryHandler(ILogger<GetCustomUrlQueryHandler> logger, AcaraCyouDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Result<List<GetCustomUrlResponse>>> Handle(GetCustomUrlQuery request, CancellationToken cancellationToken)
        {
            List<GetCustomUrlResponse> customUrls = await _dbContext.Event
                .AsNoTracking()
                .Select(dbEvent => new GetCustomUrlResponse()
                {
                    EventId = dbEvent.Id,
                    FormCustomUrl = dbEvent.FormCustomUrl,
                    PageCustomUrl = dbEvent.PageCustomUrl
                })
                .ToListAsync(cancellationToken);


            return Result.Ok(customUrls);
        }
    }
}
