using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.GoogleLogin.Challenge
{
    public class ChallengeRequestValidator : Validator<ChallengeRequest>
    {
        public ChallengeRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Query)
                .NotNull();

            When(request => request.Query is not null, () =>
            {
                RuleFor(request => request.Query!.ReturnUrl)
                    .NotNull()
                    .Must(returnUrl => returnUrl.StartsWith('/'))
                    .WithMessage("returnUrl must start with slash '/'");
            });
        }
    }
}
