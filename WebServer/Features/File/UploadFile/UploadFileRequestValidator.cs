using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.File.UploadFile
{
    public class UploadFileRequestValidator : Validator<UploadFileRequest>
    {
        public UploadFileRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Body)
                .NotNull();

            When(request => request.Body is not null, () =>
            {
                RuleFor(request => request.Body.File)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .Must(file => (file.Length <= (1024 * 1024) * 5) && (file.Length > 0))
                    .WithMessage("minimum file size is 1Byte, maximum file size is 5MB");
            });
        }
    }
}
