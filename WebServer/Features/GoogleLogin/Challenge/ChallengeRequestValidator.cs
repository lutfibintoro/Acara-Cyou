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
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .Must(returnUrl => returnUrl.StartsWith("https://") || returnUrl.StartsWith("http://"))
                    .WithMessage("returnUrl must start with 'https://' or 'http://'");
            });
        }
    }
}
