using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.CustomUrl.UpdateCustomUrl
{
    public class UpdateCustomUrlRequestValidator : Validator<UpdateCustomUrlRequest>
    {
        public UpdateCustomUrlRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Body)
                .NotNull();

            When(request => request.Body is not null, () =>
            {
                RuleFor(request => request.Body.EventId)
                    .NotNull();

                RuleFor(request => request.Body.FormCustomUrl)
                    .NotNull()
                    .Matches(@"^[a-z0-9-_]+$");

                RuleFor(request => request.Body.PageCustomUrl)
                    .NotNull()
                    .Matches(@"^[a-z0-9-_]+$");
            });
        }
    }
}
