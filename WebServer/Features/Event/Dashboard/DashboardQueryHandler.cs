using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.Data.Entities;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.Event.Dashboard
{
    public class DashboardQueryHandler : IRequestHandler<DashboardQuery, Result<DashboardResponse>>
    {
        private const int DailySeriesLength = 10;

        private readonly ILogger<DashboardQueryHandler> _logger;
        private readonly AcaraCyouDbContext _acaraCyouDbContext;

        public DashboardQueryHandler(ILogger<DashboardQueryHandler> logger, AcaraCyouDbContext acaraCyouDbContext)
        {
            _logger = logger;
            _acaraCyouDbContext = acaraCyouDbContext;
        }

        public async Task<Result<DashboardResponse>> Handle(DashboardQuery request, CancellationToken cancellationToken)
        {
            var existingEvent = await _acaraCyouDbContext.Event
                .AsNoTracking()
                .Where(dbEvent =>
                    dbEvent.Id == request.EventId &&
                    dbEvent.AcaraCyouUserId == request.UserId)
                .Select(dbEvent => new { dbEvent.IsDeleted })
                .FirstOrDefaultAsync(cancellationToken);

            if (existingEvent is null)
            {
                NotFoundError<string> resultError = new("eventId", $"{request.EventId}", null, Severity.Error);
                return Result.Fail(resultError);
            }

            if (existingEvent.IsDeleted)
            {
                ValidationError<string> resultError = new("eventId", $"{request.EventId}", $"Event [{request.EventId}] has been deleted and cannot be shown", Severity.Error);
                return Result.Fail(resultError);
            }

            List<TicketStatistic> tickets = await _acaraCyouDbContext.TicketCategory
                .AsNoTracking()
                .Where(ticketCategory =>
                    ticketCategory.EventId == request.EventId &&
                    !ticketCategory.IsDeleted)
                .Select(ticketCategory => new TicketStatistic()
                {
                    Category = ticketCategory.Category,
                    Quota = ticketCategory.Quota,
                    BookedQuota = ticketCategory.BookedQuota,
                })
                .ToListAsync(cancellationToken);

            var statusCounts = await _acaraCyouDbContext.Order
                .AsNoTracking()
                .Where(order => order.OrderItems.Any(orderItem => orderItem.TicketCategory.EventId == request.EventId))
                .GroupBy(order => order.Status)
                .Select(orderGroup => new { Status = orderGroup.Key, Count = orderGroup.Count() })
                .ToListAsync(cancellationToken);

            int totalSuccessOrder = statusCounts.FirstOrDefault(statusCount => statusCount.Status == OrderStatus.Success)?.Count ?? 0;
            int totalPendingOrder = statusCounts.FirstOrDefault(statusCount => statusCount.Status == OrderStatus.Pending)?.Count ?? 0;

            decimal totalNetRevenue = await _acaraCyouDbContext.Order
                .AsNoTracking()
                .Where(order =>
                    order.Status == OrderStatus.Success &&
                    order.OrderItems.Any(orderItem => orderItem.TicketCategory.EventId == request.EventId))
                .SumAsync(order => order.GrandTotal - order.ApplicationFee, cancellationToken);

            DateTime firstDayUtc = DateTime.UtcNow.Date.AddDays(-(DailySeriesLength - 1));

            List<DateTime> successCreatedAts = await _acaraCyouDbContext.Order
                .AsNoTracking()
                .Where(order =>
                    order.Status == OrderStatus.Success &&
                    order.CreatedAt >= firstDayUtc &&
                    order.OrderItems.Any(orderItem => orderItem.TicketCategory.EventId == request.EventId))
                .Select(order => order.CreatedAt)
                .ToListAsync(cancellationToken);

            Dictionary<DateTime, int> successCountPerDay = successCreatedAts
                .GroupBy(createdAt => createdAt.Date)
                .ToDictionary(createdAtGroup => createdAtGroup.Key, createdAtGroup => createdAtGroup.Count());

            List<DaySuccessRegistration> dailySuccessRegistrations = Enumerable.Range(0, DailySeriesLength)
                .Select(dayOffset => firstDayUtc.AddDays(dayOffset))
                .Select(day => new DaySuccessRegistration()
                {
                    Date = day.ToString("yyyy-MM-dd"),
                    TotalSuccess = successCountPerDay.TryGetValue(day, out int successCount) ? successCount : 0,
                })
                .ToList();

            _logger.LogInformation("Dashboard of event [{EventId}] was loaded by user [{UserId}]", request.EventId, request.UserId);

            return Result.Ok(new DashboardResponse()
            {
                TotalNetRevenue = totalNetRevenue,
                Tickets = tickets,
                TotalSuccessOrder = totalSuccessOrder,
                TotalPendingOrder = totalPendingOrder,
                DailySuccessRegistrations = dailySuccessRegistrations,
            });
        }
    }
}
