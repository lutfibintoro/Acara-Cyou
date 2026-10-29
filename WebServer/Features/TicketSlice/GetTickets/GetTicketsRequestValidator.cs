using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.TicketSlice.GetTickets
{
    public class GetTicketsRequestValidator : Validator<GetTicketsRequest>
    {
        public GetTicketsRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.EventId)
                .NotNull();
        }
    }
}
