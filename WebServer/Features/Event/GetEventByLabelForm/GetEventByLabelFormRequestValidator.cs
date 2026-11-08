using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.Event.GetEventByLabelForm
{
    public class GetEventByLabelFormRequestValidator : Validator<GetEventByLabelFormRequest>
    {
        public GetEventByLabelFormRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Query)
                .NotNull();

            When(request => request.Query is not null, () =>
            {
                RuleFor(request => request.Query.CustomUrlName)
                    .NotNull()
                    .Matches(@"^[a-z0-9-_]+$");
            });
        }
    }
}
