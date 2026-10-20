using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.GoogleLogin.KeyExchange
{
    public class KeyExchangeRequestValidator : Validator<KeyExchangeRequest>
    {
        public KeyExchangeRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Query)
                .NotNull();

            When(request => request.Query is not null, () =>
            {
                RuleFor(request => request.Query!.Key)
                    .NotNull()
                    .NotEmpty()
                    .Length(Guid.NewGuid().ToString().Length);
            });
        }
    }
}
