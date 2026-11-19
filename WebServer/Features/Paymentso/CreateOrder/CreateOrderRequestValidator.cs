using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.Paymentso.CreateOrder
{
    public class CreateOrderRequestValidator : Validator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Query)
                .NotNull();

            RuleFor(request => request.Body)
                .NotNull();

            When(request => request.Query is not null, () =>
            {
                RuleFor(request => request.Query.EventId)
                    .NotNull();
            });

            When(request => request.Body is not null, () =>
            {
                RuleFor(request => request.Body.PaymentGatewayId)
                    .NotNull();

                RuleFor(request => request.Body.PaymentMethodId)
                    .NotNull();
                
                RuleFor(request => request.Body.TicketId)
                    .NotNull();
                
                RuleFor(request => request.Body.UserEmailAddress)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .EmailAddress();
                
                RuleFor(request => request.Body.FormTemplateValueBase64)
                    .NotNull();
            });
        }
    }
}
