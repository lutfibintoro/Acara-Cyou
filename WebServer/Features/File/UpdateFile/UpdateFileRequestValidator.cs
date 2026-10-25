using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.File.UpdateFile
{
    public class UpdateFileRequestValidator : Validator<UpdateFileRequest>
    {
        public UpdateFileRequestValidator()
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

                RuleFor(request => request.Body.FileName)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .Must(fileName => fileName.StartsWith("./UserUploads/"))
                    .WithMessage("fileName must start with './UserUploads/'")
                    .Must(fileName => fileName.Contains('+'))
                    .WithMessage("fileName must contain '+'");
            });
        }
    }
}
