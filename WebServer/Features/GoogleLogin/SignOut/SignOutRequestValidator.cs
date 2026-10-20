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

            RuleFor(request => request.Body)
                .NotNull();

            When(request => request.Body is not null, () =>
            {
                RuleFor(request => request.Body!.RefreshToken)
                    .NotNull()
                    .Length(Base64Url.EncodeToString(Guid.NewGuid().ToByteArray()).Length);
            });
        }
    }
}
