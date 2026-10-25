using System.Buffers.Text;
using FastEndpoints;
using FluentValidation;
using MediatR;

namespace WebServer.Features.Event.AddEvent
{
    public class AddEventRequestValidator : Validator<AddEventRequest>
    {
        public AddEventRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Body)
                .NotNull();

            When(request => request.Body is not null, () =>
            {
                RuleFor(request => request.Body.Title)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .MinimumLength(1);

                RuleFor(request => request.Body.Description)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .MinimumLength(1);

                When(request => request.Body.Posters is not null && request.Body.Posters.Count > 0, () =>
                {
                    RuleForEach(request => request.Body.Posters).ChildRules(poster =>
                    {
                        poster.RuleFor(p => p.FullPathUrl)
                            .Cascade(CascadeMode.Stop)
                            .NotNull()
                            .Must(fullPathUrl => fullPathUrl.StartsWith("https://") || fullPathUrl.StartsWith("http://"))
                            .WithMessage("fullPathUrl must start with 'https://' or 'http://'");

                        poster.RuleFor(p => p.MimeType)
                            .Cascade(CascadeMode.Stop)
                            .NotNull();

                        poster.RuleFor(p => p.ByteLength)
                            .GreaterThan(0u);
                    });
                });
            });
        }
    }
}