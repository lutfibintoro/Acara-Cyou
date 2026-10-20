using System.Buffers.Text;
using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.GoogleLogin.SignOut
{
    public class SignOutRequestValidator : Validator<SignOutRequest>
    {
        public SignOutRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Query)
                .NotNull();

            When(request => request.Query is not null, () =>
            {
                RuleFor(request => request.Query!.RefreshToken)
                    .NotNull()
                    .Length(Base64Url.EncodeToString(Guid.NewGuid().ToByteArray()).Length);
            });
        }
    }
}
