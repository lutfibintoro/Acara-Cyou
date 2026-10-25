using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.Event.Dashboard
{
    public class DashboardRequestValidator : Validator<DashboardRequest>
    {
        public DashboardRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.EventId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("route parameter [eventId] is required and must not be an empty guid");
        }
    }
}
