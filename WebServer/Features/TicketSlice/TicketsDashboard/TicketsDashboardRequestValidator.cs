using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.TicketSlice.TicketsDashboard
{
    public class TicketsDashboardRequestValidator : Validator<TicketsDashboardRequest>
    {
        public TicketsDashboardRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.EventId)
                .NotNull();
        }
    }
}
