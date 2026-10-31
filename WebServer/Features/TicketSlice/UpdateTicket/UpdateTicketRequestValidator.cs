using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.TicketSlice.UpdateTicket
{
    public class UpdateTicketRequestValidator : Validator<UpdateTicketRequest>
    {
        public UpdateTicketRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.TicketId)
                .NotNull();

            RuleFor(request => request.Body)
                .NotNull();

            When(request => request.Body is not null, () =>
            {
                RuleFor(request => request.Body.Category)
                    .NotNull();
                RuleFor(request => request.Body.ParticipantNotice)
                    .NotNull();
                RuleFor(request => request.Body.PriceOne)
                    .NotNull();
                RuleFor(request => request.Body.Quota)
                    .NotNull();
                RuleFor(request => request.Body.BookedQuota)
                    .NotNull();
                RuleFor(request => request.Body.IsSalesOpen)
                    .NotNull();
                RuleFor(request => request.Body.OpenSalesDate)
                    .NotNull()
                    .LessThan(request => request.Body.CloseSalesDate);
                RuleFor(request => request.Body.CloseSalesDate)
                    .NotNull()
                    .GreaterThan(request => request.Body.OpenSalesDate);
                RuleFor(request => request.Body.IsDeleted)
                    .NotNull();
            });
        }
    }
}
