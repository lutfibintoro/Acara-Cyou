using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.Event.DeleteEventById
{
    public class DeleteEventByIdRequestValidator : Validator<DeleteEventByIdRequest>
    {
        public DeleteEventByIdRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.EventId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("query string parameter [eventId] is required and must not be an empty guid");
        }
    }
}
