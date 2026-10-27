using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.Poster.UpdatePosterById
{
    public class UpdatePosterByIdRequestValidator : Validator<UpdatePosterByIdRequest>
    {
        public UpdatePosterByIdRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Body)
                .NotNull();

            When(request => request.Body is not null, () =>
            {
                RuleFor(p => p.Body.PosterId)
                    .NotNull();

                RuleFor(p => p.Body.ByteLength)
                    .GreaterThan(0u);
            });
        }
    }
}
