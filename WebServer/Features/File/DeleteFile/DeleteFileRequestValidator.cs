using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.File.DeleteFile
{
    public class DeleteFileRequestValidator : Validator<DeleteFileRequest>
    {
        public DeleteFileRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Query)
                .NotNull();

            When(request => request.Query is not null, () =>
            {
                RuleFor(request => request.Query!.FileName)
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
