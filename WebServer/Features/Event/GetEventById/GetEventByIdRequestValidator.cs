using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.Event.GetEventById
{
    public class GetEventByIdRequestValidator : Validator<GetEventByIdRequest>
    {
        public GetEventByIdRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.EventId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("route parameter [eventId] is required and must not be an empty guid");
        }
    }
}
