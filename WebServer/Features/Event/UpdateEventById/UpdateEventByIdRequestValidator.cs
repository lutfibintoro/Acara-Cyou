using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.Event.UpdateEventById
{
    public class UpdateEventByIdRequestValidator : Validator<UpdateEventByIdRequest>
    {
        public UpdateEventByIdRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Body)
                .NotNull();

            When(request => request.Body is not null, () =>
            {
                RuleFor(request => request.Body)
                    .Must(body =>
                        body.OpenRegistrationDate != default(DateTime) &&
                        body.CloseRegistrationDate != default(DateTime) &&
                        body.CloseRegistrationDate >= body.OpenRegistrationDate)
                    .WithMessage("openRegistrationDate and closeRegistrationDate are required (not empty) and closeRegistrationDate must be greater than or equal to openRegistrationDate");
            });
        }
    }
}
