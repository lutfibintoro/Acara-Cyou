using FluentResults;
using FluentValidation;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.Data.Entities;
using WebServer.Infrastructures.ResultError;
using WebServer.Shared.Payments.CreatePayment;

namespace WebServer.Features.Paymentso.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<CreateOrderResponse>>
    {
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly DateTime _indonesiaTimeNow;

        public CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger, AcaraCyouDbContext dbContext, IHttpContextAccessor contextAccessor, IBackgroundJobClient backgroundJobClient)
        {
            _logger = logger;
            _dbContext = dbContext;
            TimeZoneInfo wibZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta");
            _indonesiaTimeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, wibZone);
            _contextAccessor = contextAccessor;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<Result<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (await IsNotConsistenceUserAsync(request.UserId, request.UserEmailAddress, cancellationToken))
            {
                ForbiddenError<string> forbiddenError = new("UserEmailAddress", request.UserEmailAddress, $"user with email {request.UserEmailAddress} not consistence with id {request.UserId}", Severity.Error);
                return Result.Fail<CreateOrderResponse>(forbiddenError);
            }

            Result<Infrastructures.Data.Entities.Event> eventResult = await GetEventWithTheClientAsync(request.EventId, request.TicketId, request.UserId, cancellationToken);
            if (eventResult.IsFailed)
                return eventResult.ToResult<CreateOrderResponse>();

            var myEvent = eventResult.Value;
            var myTicket = myEvent.TicketCategorys.First();
            var myOrderItems = myTicket.OrderItems;
            var myPaymentMethod = await _dbContext.PaymentMethod
                .AsNoTracking()
                .Where(dbPaymentMethod => dbPaymentMethod.Id == request.PaymentMethodId)
                .Include(dbPaymentMethod => dbPaymentMethod.PaymentGateway)
                .FirstAsync(cancellationToken);

            if (IsUserHasSuccessOrder(myOrderItems))
            {
                Order successOrder = myOrderItems.Where(orderItem => orderItem.Order.Status == OrderStatus.Success).First().Order;
                DuplicateError<string> duplicateError = new("orderId", $"{successOrder.Id}", $"{request.UserEmailAddress} user has successfully order with order id {successOrder.Id}", Severity.Error);
                return Result.Fail<CreateOrderResponse>(duplicateError);
            }

            if (IsUserHasPendingOrder(myOrderItems))
            {
                Order pendingOrder = myOrderItems.Where(orderItem => orderItem.Order.Status == OrderStatus.Pending).First().Order;
                return Result.Ok(new CreateOrderResponse() { OrderId = pendingOrder.Id });
            }

            if (IsEventOrTicketNoLongerActive(myEvent, myTicket))
            {
                SalesClosedError<string> salesClosedError = new("event or ticket", $"{myEvent.Id} or {myTicket.Id}", null, Severity.Error);
                return Result.Fail<CreateOrderResponse>(salesClosedError);
            }

            decimal discountAmount = 0m;
            decimal applicationFee = 500m;
            decimal totalItemPrice = CalculateTotalItemPrice(1, myTicket.PriceOne);
            decimal grandTotal = CalculateGrandTotal(totalItemPrice, discountAmount, applicationFee);
            decimal amountDue = CalculateAmountDue(myPaymentMethod, grandTotal);
            decimal pgTotalFee = CalculatePgTotalFee(myPaymentMethod, grandTotal);

            if (amountDue <= 0m)
            {
                Result<Guid> createFreeOrderResult = await CreateNewFreeOrderAsync(request.FormTemplateValue, request.UserId, request.TicketId, totalItemPrice, grandTotal, amountDue, discountAmount, cancellationToken);
                if (createFreeOrderResult.IsFailed)
                    return createFreeOrderResult.ToResult<CreateOrderResponse>();
                return Result.Ok(new CreateOrderResponse() { OrderId = createFreeOrderResult.Value });
            }

            Result<Guid> createHangingOrderResult = await CreateNewHangingOrderAsync(request.FormTemplateValue, request.UserId, totalItemPrice, grandTotal, discountAmount, applicationFee, cancellationToken);
            if (createHangingOrderResult.IsFailed)
                return createHangingOrderResult.ToResult<CreateOrderResponse>();

            HttpContext httpContext = _contextAccessor.HttpContext!;
            string baseAddress = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
            _backgroundJobClient.Enqueue<CreateOrderBackgroundJobs>(x => x.CreatePendingPayment(myPaymentMethod, myTicket.Category, request.UserEmailAddress, baseAddress, request.PaymentGatewayId, createHangingOrderResult.Value, request.TicketId, amountDue, pgTotalFee));
            return Result.Ok(new CreateOrderResponse() { OrderId = createHangingOrderResult.Value });
        }

        #region consistence user
        private async Task<bool> IsNotConsistenceUserAsync(Guid userId, string userEmailAddress, CancellationToken ct)
        {
            AcaraCyouUser? user = await _dbContext.AcaraCyouUser
                .AsNoTracking()
                .Where(dbUser => dbUser.Id == userId)
                .Where(dbUser => dbUser.Email == userEmailAddress)
                .FirstOrDefaultAsync(ct);

            if (user is null)
                return true;
            return false;
        }
        #endregion

        #region event
        private async Task<Result<Infrastructures.Data.Entities.Event>> GetEventWithTheClientAsync(Guid eventId, Guid ticketId, Guid userId, CancellationToken ct)
        {
            Infrastructures.Data.Entities.Event? myEvent = await _dbContext.Event
                .AsNoTracking()
                .Where(dbEvent => dbEvent.Id == eventId)
                .Include(dbEvent => dbEvent.TicketCategorys
                    .Where(dbTicket => dbTicket.Id == ticketId))
                    .ThenInclude(dbTicket => dbTicket.OrderItems
                        .Where(dbOrderItem =>
                            dbOrderItem.Order.AcaraCyouUserId == userId &&
                            (dbOrderItem.Order.Status == OrderStatus.Pending ||
                            dbOrderItem.Order.Status == OrderStatus.Success)))
                        .ThenInclude(dbOrderItem => dbOrderItem.Order)
                            .ThenInclude(dbOrder => dbOrder.Invoice)
                                .ThenInclude(dbInvoice => dbInvoice!.PaymentDetail)
                .AsSplitQuery()
                .FirstOrDefaultAsync(ct);

            if (myEvent is null)
            {
                NotFoundError<string> resultError = new("eventId", $"{eventId}", null, Severity.Error);
                return Result.Fail<Infrastructures.Data.Entities.Event>(resultError);
            }

            if (myEvent.TicketCategorys.Count == 0)
            {
                NotFoundError<string> resultError = new("ticketId", $"{ticketId}", null, Severity.Error);
                return Result.Fail<Infrastructures.Data.Entities.Event>(resultError);
            }

            return Result.Ok(myEvent);
        }
        #endregion

        #region order validation
        private static bool IsUserHasPendingOrder(ICollection<OrderItem> orderItems)
        {
            if (orderItems.Any(orderItem => orderItem.Order.Status == OrderStatus.Pending))
                return true;
            return false;
        }

        private static bool IsUserHasSuccessOrder(ICollection<OrderItem> orderItems)
        {
            if (orderItems.Any(orderItem => orderItem.Order.Status == OrderStatus.Success))
                return true;
            return false;
        }

        private bool IsEventOrTicketNoLongerActive(Infrastructures.Data.Entities.Event myEvent, TicketCategory myTicket)
        {
            bool eventIsActive = myEvent.IsDeleted == false &&
                myEvent.IsRegistrationOpen == true &&
                myEvent.OpenRegistrationDate <= _indonesiaTimeNow && myEvent.CloseRegistrationDate >= _indonesiaTimeNow;

            if (!eventIsActive)
                return true;

            bool ticketIsActive = myTicket.IsDeleted == false &&
                myTicket.IsSalesOpen == true &&
                myTicket.OpenSalesDate <= _indonesiaTimeNow && myTicket.CloseSalesDate >= _indonesiaTimeNow;

            if (!ticketIsActive)
                return true;

            bool ticketIsFull = myTicket.BookedQuota >= myTicket.Quota;
            if (ticketIsFull)
                return true;

            return false;
        }
        #endregion

        #region order
        private async Task<Result<Guid>> CreateNewHangingOrderAsync(string formTemplateValue, Guid userId, decimal totalItemPrice, decimal grandTotal, decimal discountAmount, decimal applicationFee, CancellationToken ct)
        {
            var newOrder = new Order()
            {
                AcaraCyouUserId = userId,
                TotalItemPrice = totalItemPrice,
                DiscountAmount = discountAmount,
                ApplicationFee = applicationFee,
                GrandTotal = grandTotal,
                Status = OrderStatus.Pending,
                FormTemplateValue = formTemplateValue,
                IsCheckedIn = false,
                IsEmailSend = false
            };

            _dbContext.Order.Add(newOrder);
            await _dbContext.SaveChangesAsync(ct);
            _dbContext.ChangeTracker.Clear();

            return Result.Ok(newOrder.Id);
        }

        private async Task<Result<Guid>> CreateNewFreeOrderAsync(string formTemplateValue, Guid userId, Guid ticketId, decimal totalItemPrice, decimal grandTotal, decimal amountDue, decimal discountAmount, CancellationToken ct)
        {
            TicketCategory ticket = await _dbContext.TicketCategory
                .Where(dbTicket => dbTicket.Id == ticketId)
                .FirstAsync(ct);

            var newOrder = new Order()
            {
                AcaraCyouUserId = userId,
                TotalItemPrice = totalItemPrice,
                DiscountAmount = discountAmount,
                ApplicationFee = 0m,
                GrandTotal = grandTotal,
                Status = OrderStatus.Success,
                FormTemplateValue = formTemplateValue,
                IsCheckedIn = false,
                IsEmailSend = false,
                OrderItems =
                [
                    new OrderItem()
                    {
                        TicketCategoryId = ticketId,
                        Quantity = 1,
                        PriceOne = ticket.PriceOne
                    }
                ],
                Invoice = new Invoice()
                {
                    AmountDue = amountDue,
                    Status = InvoiceStatus.Paid,
                    PaymentDetail = new PaymentDetail()
                    {
                        PgFee = 0m,
                        PaidTotal = 0m,
                        Status = PaymentDetailStatus.Settlement
                    }
                }
            };

            _dbContext.Order.Add(newOrder);
            for (int i = 0; i < 10; i++)
            {
                bool ticketIsFull = ticket.BookedQuota >= ticket.Quota;
                if (ticketIsFull)
                {
                    SalesClosedError<string> salesClosedError = new("ticket", $"{ticket.Category}", null, Severity.Error);
                    return Result.Fail<Guid>(salesClosedError);
                }

                try
                {
                    ticket.BookedQuota++;
                    ticket.RowVersion++;
                    await _dbContext.SaveChangesAsync(ct);
                    return Result.Ok(newOrder.Id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    await Task.Delay(200, ct);
                    await _dbContext.Entry(ticket).ReloadAsync(ct);
                }
            }

            ConcurrencyError<string> concurrenResultError = new("ticketId", $"{ticketId}", $"ticket [{ticketId}] was concurrently modified by another request", Severity.Error);
            return Result.Fail<Guid>(concurrenResultError);
        }
        #endregion

        #region calculate
        private static decimal CalculateTotalItemPrice(int quantity, decimal priceOne)
        {
            return quantity * priceOne;
        }

        private static decimal CalculateGrandTotal(decimal totalItemPrice, decimal discountAmount, decimal applicationFee)
        {
            if (totalItemPrice <= 0)
                return 0m;

            if ((totalItemPrice - discountAmount) <= 0m)
                return 0m;

            return totalItemPrice - discountAmount + applicationFee;
        }

        private static decimal CalculateAmountDue(PaymentMethod paymentMethod, decimal grandTotal)
        {
            if (grandTotal <= 0m)
                return 0m;

            if (paymentMethod.AdminFeeBearer == PaymentMethodAdminFeeBearer.Merchant)
            {
                decimal flatFee = paymentMethod.FlatFee;
                decimal percentageFee = 1m - (paymentMethod.PercentageFee / 100m);
                decimal ceiling = Math.Ceiling((grandTotal + flatFee) / percentageFee);
                return ceiling;
            }

            if (paymentMethod.AdminFeeBearer == PaymentMethodAdminFeeBearer.Customer)
            {
                return grandTotal;
            }

            return 0m;
        }

        private static decimal CalculatePgTotalFee(PaymentMethod paymentMethod, decimal grandTotal)
        {
            if (grandTotal <= 0m)
                return 0m;

            if (paymentMethod.AdminFeeBearer == PaymentMethodAdminFeeBearer.Merchant)
            {
                decimal flatFee = paymentMethod.FlatFee;
                decimal percentageFee = 1m - (paymentMethod.PercentageFee / 100m);
                decimal ceiling = Math.Ceiling(((grandTotal + flatFee) / percentageFee) - grandTotal);
                return ceiling;
            }

            if (paymentMethod.AdminFeeBearer == PaymentMethodAdminFeeBearer.Customer)
            {
                decimal flatFee = paymentMethod.FlatFee;
                decimal percentageFee = grandTotal * (paymentMethod.PercentageFee / 100m);
                decimal ceiling = Math.Ceiling((grandTotal + flatFee + percentageFee) - grandTotal);
                return ceiling;
            }

            return 0m;
        }
        #endregion
    }
}