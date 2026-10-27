using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.Poster.AddPoster
{
    public class AddPosterRequestValidator : Validator<AddPosterRequest>
    {
        public AddPosterRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Body)
                .NotNull();

            When(request => request.Body is not null, () =>
            {
                RuleFor(request => request.Body.EventId)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty();

                RuleFor(request => request.Body.FullPathUrl)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .Must(fullPathUrl => fullPathUrl.StartsWith("https://") || fullPathUrl.StartsWith("http://"))
                    .WithMessage("fullPathUrl must start with 'https://' or 'http://'");

                RuleFor(request => request.Body.MimeType)
                    .Cascade(CascadeMode.Stop)
                    .NotNull();

                RuleFor(request => request.Body.ByteLength)
                    .GreaterThan(0u);
            });
        }
    }
}
