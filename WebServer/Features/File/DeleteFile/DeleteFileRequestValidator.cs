using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.File.DeleteFile
{
    public class DeleteFileRequestValidator : Validator<DeleteFileRequest>
    {
        public DeleteFileRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Body)
                .NotNull();

            When(request => request.Body is not null, () =>
            {
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
