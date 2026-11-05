using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.Paymentso.GetMethod
{
    public class GetMethodQueryHandler : IRequestHandler<GetMethodQuery, Result<GetMethodResponse>>
    {
        private readonly ILogger<GetMethodQueryHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;
        private const decimal _applicationFlatFee = 500m;

        public GetMethodQueryHandler(ILogger<GetMethodQueryHandler> logger, AcaraCyouDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Result<GetMethodResponse>> Handle(GetMethodQuery request, CancellationToken cancellationToken)
        {
            GetMethodResponse? response = await _dbContext.PaymentGateway
                .Where(db => db.Name == Infrastructures.Data.Entities.PaymentGatewayName.Duitku)
                .Select(db => new GetMethodResponse()
                {
                    PaymentGatewayId = db.Id,
                    PaymentMethods = db.PaymentMethods
                        .Select(dbPaymentMethod => new PaymentMethod()
                        {
                            PaymentMethodId = dbPaymentMethod.Id,
                            Type = dbPaymentMethod.Type,
                            Name = dbPaymentMethod.Name,
                            FlatFee = dbPaymentMethod.FlatFee,
                            PercentageFee = dbPaymentMethod.PercentageFee,
                            ApplicationFlatFee = _applicationFlatFee
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (response is null)
            {
                var resultFail = new NotFoundError<string>("payment gateway", "", null, FluentValidation.Severity.Error);
                return Result.Fail<GetMethodResponse>(resultFail);
            }

            return Result.Ok(response);
        }
    }
}
