using System.Text;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.Data.Entities;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.TicketSlice.AddTicket
{
    public class AddTicketCommandHandler : IRequestHandler<AddTicketCommand, Result<AddTicketResponse>>
    {
        private readonly ILogger<AddTicketCommandHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;

        public AddTicketCommandHandler(ILogger<AddTicketCommandHandler> logger, AcaraCyouDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Result<AddTicketResponse>> Handle(AddTicketCommand request, CancellationToken cancellationToken)
        {
            byte[] byteTeks = Convert.FromBase64String(request.ParticipantNotice);
            request.ParticipantNotice = Encoding.UTF8.GetString(byteTeks);

            Infrastructures.Data.Entities.Event? myEvent = await _dbContext.Event
                .AsNoTracking()
                .Where(dbEvent => dbEvent.Id == request.EventId)
                .Select(dbEvent => new Infrastructures.Data.Entities.Event()
                {
                    Id = dbEvent.Id,
                    AcaraCyouUserId = dbEvent.AcaraCyouUserId
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (myEvent is null)
            {
                NotFoundError<string> notFoundResultError = new("eventId", $"{request.EventId}", null, Severity.Error);
                return Result.Fail<AddTicketResponse>(notFoundResultError);
            }

            if (myEvent.AcaraCyouUserId != request.UserId)
            {
                ForbiddenError<string> forbiddenResultError = new("userId", $"{request.UserId}", null, Severity.Error);
                return Result.Fail<AddTicketResponse>(forbiddenResultError);
            }

            var ticket = new TicketCategory()
            {
                Category = request.Category,
                CloseSalesDate = request.CloseSalesDate,
                OpenSalesDate = request.OpenSalesDate,
                EventId = request.EventId,
                BookedQuota = request.BookedQuota,
                Quota = request.Quota,
                ParticipantNotice = request.ParticipantNotice,
                IsSalesOpen = request.IsSalesOpen,
                PriceOne = request.PriceOne,
                IsDeleted = false
            };

            _dbContext.TicketCategory.Add(ticket);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(new AddTicketResponse() { TicketId = ticket.Id });
        }
    }
}
