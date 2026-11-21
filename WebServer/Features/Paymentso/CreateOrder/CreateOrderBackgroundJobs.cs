using System.Net.Sockets;
using FluentResults;
using FluentValidation;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.Data.Entities;
using WebServer.Infrastructures.ResultError;
using WebServer.Shared.Payments.CreatePayment;

namespace WebServer.Features.Paymentso.CreateOrder
{
    public class CreateOrderBackgroundJobs
    {
        private readonly ILogger<CreateOrderBackgroundJobs> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IEnumerable<IPaymentGatewayCreate> _paymentGatewayCreates;
        private readonly DateTime _indonesiaTimeNow;

        public CreateOrderBackgroundJobs(ILogger<CreateOrderBackgroundJobs> logger, IServiceScopeFactory scopeFactory, IBackgroundJobClient backgroundJobClient, IEnumerable<IPaymentGatewayCreate> paymentGatewayCreates)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _backgroundJobClient = backgroundJobClient;
            _paymentGatewayCreates = paymentGatewayCreates;
            TimeZoneInfo wibZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta");
            _indonesiaTimeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, wibZone);
        }

        public async Task CreatePendingPayment(PaymentMethod paymentMethod, string category, string customerEmail, string baseAddress, Guid paymentGatewayId, Guid orderId, Guid ticketId, decimal amountDue, decimal pgTotalFee)
        {
            await using (AsyncServiceScope scope = _scopeFactory.CreateAsyncScope())
            {
                IServiceProvider provider = scope.ServiceProvider;
                AcaraCyouDbContext dbContext = provider.GetService<AcaraCyouDbContext>()!;
                PaymentGateway paymentGateway = await dbContext.PaymentGateway
                    .AsNoTracking()
                    .Where(dbpg => dbpg.Id == paymentGatewayId)
                    .FirstAsync();

                Result<PaymentGatewayCreateResponse> createPaymentResult = await CreatePaymentAsync(paymentGateway, paymentMethod, baseAddress, amountDue, orderId, category, customerEmail);
                if (createPaymentResult.IsFailed)
                {
                    await CancelOrderAsync(dbContext, orderId);
                    return;
                }

                Result commitResult = await CommitOrderAsync(paymentGateway, paymentMethod, createPaymentResult.Value, dbContext, orderId, ticketId, amountDue, pgTotalFee);
                if (commitResult.IsFailed)
                {
                    dbContext.ChangeTracker.Clear();
                    await CancelOrderAsync(dbContext, orderId);
                    return;
                }

                _backgroundJobClient.Schedule<CreateOrderBackgroundJobs>(
                    x => x.DelayedCheck(orderId, ticketId),
                    TimeSpan.FromMinutes(paymentMethod.MaxExpiredInMinute + 1));
            }
        }

        public async Task DelayedCheck(Guid orderId, Guid ticketId)
        {
            await using (AsyncServiceScope scope = _scopeFactory.CreateAsyncScope())
            {
                IServiceProvider provider = scope.ServiceProvider;
                AcaraCyouDbContext dbContext = provider.GetService<AcaraCyouDbContext>()!;

                TicketCategory ticket = await dbContext.TicketCategory
                    .Where(dbTicket => dbTicket.Id == ticketId)
                    .FirstAsync();

                Order order = await dbContext.Order
                    .Where(dbOrder => dbOrder.Id == orderId)
                    .FirstAsync();

                if (order.Status == OrderStatus.Success)
                    return;

                Invoice invoice = await dbContext.Invoice
                    .Where(dbInvoice => dbInvoice.OrderId == orderId)
                    .FirstAsync();

                PaymentDetail paymentDetail = await dbContext.PaymentDetail
                    .Where(dbPaymentDetail => dbPaymentDetail.InvoiceId == invoice.Id)
                    .FirstAsync();

                while (true)
                {
                    try
                    {
                        ticket.BookedQuota--;
                        ticket.RowVersion++;

                        invoice.Status = InvoiceStatus.Expired;
                        invoice.UpdatedAt = DateTime.UtcNow;
                        invoice.RowVersion++;

                        order.Status = OrderStatus.Cancelled;
                        order.UpdatedAt = DateTime.UtcNow;
                        order.RowVersion++;

                        paymentDetail.Status = PaymentDetailStatus.Expired;
                        paymentDetail.UpdatedAt = DateTime.UtcNow;
                        paymentDetail.RowVersion++;

                        await dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        await Task.Delay(200);
                        await dbContext.Entry(ticket).ReloadAsync();
                        await dbContext.Entry(order).ReloadAsync();
                        await dbContext.Entry(invoice).ReloadAsync();
                        await dbContext.Entry(paymentDetail).ReloadAsync();
                    }
                }
            }
        }

        #region payment
        private async Task<Result<PaymentGatewayCreateResponse>> CreatePaymentAsync(PaymentGateway paymentGateway, PaymentMethod paymentMethod, string baseAddress, decimal amountDue, Guid orderId, string category, string customerEmail)
        {
            PaymentGatewayCreateName paymentGatewayName = paymentGateway.Name switch
            {
                PaymentGatewayName.Duitku => PaymentGatewayCreateName.Duitku,
                _ => PaymentGatewayCreateName.Duitku
            };

            IPaymentGatewayCreate paymentGatewayCreate = _paymentGatewayCreates
                .Where(payment => payment.PaymentGatewayName == paymentGatewayName)
                .First();

            var paymentGatewayCreateRequest = new PaymentGatewayCreateRequest()
            {
                Amount = (int)amountDue,
                OrderId = orderId.ToString(),
                ProductDetail = category,
                CustomerEmail = customerEmail,
                CodePayment = paymentMethod.Code,
                CustomerVaName = customerEmail.Split('@')[0],
                ExpiredInMinute = paymentMethod.MaxExpiredInMinute,
                CallbackUrl = $"{baseAddress}/api/duitku/hook"
            };

            Result<PaymentGatewayCreateResponse> paymentGatewayCreateResult = await paymentGatewayCreate.CreatePaymentAsync(paymentGatewayCreateRequest, default);
            return paymentGatewayCreateResult;
        }
        #endregion

        #region database write
        private async Task<Result> CommitOrderAsync(PaymentGateway paymentGateway, PaymentMethod paymentMethod, PaymentGatewayCreateResponse createPaymentResponse, AcaraCyouDbContext dbContext, Guid orderId, Guid ticketId, decimal amountDue, decimal pgTotalFee)
        {
            TicketCategory myTicket = await dbContext.TicketCategory
                .Where(dbTicket => dbTicket.Id == ticketId)
                .FirstAsync();

            Order myOrder = await dbContext.Order
                .Where(dbOrder => dbOrder.Id == orderId)
                .FirstAsync();

            myOrder.OrderItems.Add(new OrderItem()
            {
                TicketCategoryId = ticketId,
                Quantity = 1,
                PriceOne = myTicket.PriceOne
            });

            dbContext.Invoice.Add(new Invoice()
            {
                OrderId = orderId,
                AmountDue = amountDue,
                DueAt = _indonesiaTimeNow.AddMinutes(paymentMethod.MaxExpiredInMinute + 1),
                Status = InvoiceStatus.Unpaid,
                PaymentDetail = new PaymentDetail()
                {
                    PgProvider = paymentGateway.Name.ToString(),
                    PgReference = createPaymentResponse.Reference,
                    PgMethod = paymentMethod.Name,
                    PgFee = pgTotalFee,
                    PaidTotal = createPaymentResponse.Amount,
                    Va = createPaymentResponse.VaNumber,
                    QrString = createPaymentResponse.QrString,
                    Status = PaymentDetailStatus.Pending,
                    AdminFeeBearer = paymentMethod.AdminFeeBearer == PaymentMethodAdminFeeBearer.Customer ? PaymentDetailAdminFeeBearer.Customer : PaymentDetailAdminFeeBearer.Merchant
                }
            });

            for (int i = 0; true; i++)
            {
                bool ticketIsFull = myTicket.BookedQuota >= myTicket.Quota;
                if (ticketIsFull)
                {
                    SalesClosedError<string> salesClosedError = new("ticket", $"{myTicket.Category}", null, Severity.Error);
                    return Result.Fail(salesClosedError);
                }

                try
                {
                    myTicket.BookedQuota++;
                    myTicket.RowVersion++;
                    await dbContext.SaveChangesAsync();
                    return Result.Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await Task.Delay(200);
                    await dbContext.Entry(myTicket).ReloadAsync();
                }
            }

            //ConcurrencyError<string> concurrenResultError = new("ticketId", $"{ticketId}", $"ticket [{ticketId}] was concurrently modified by another request", Severity.Error);
            //return Result.Fail(concurrenResultError);
        }

        private static async Task<Result> CancelOrderAsync(AcaraCyouDbContext dbContext, Guid orderId)
        {
            Order? order = await dbContext.Order
                .Where(dbOrder => dbOrder.Id == orderId)
                .FirstOrDefaultAsync();

            if (order is null)
            {
                ValidationError<string> error = new("", "", null, Severity.Error);
                return Result.Fail(error);
            }

            while (true)
            {
                try
                {
                    order.Status = OrderStatus.Cancelled;
                    order.RowVersion++;
                    await dbContext.SaveChangesAsync();
                    return Result.Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await Task.Delay(200);
                    await dbContext.Entry(order).ReloadAsync();
                }
            }
        }
        #endregion
    }
}