using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.File.GetFile
{
    public class GetFileRequestValidator : Validator<GetFileRequest>
    {
        public GetFileRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Query)
                .NotNull();


            When(request => request.Query is not null, () =>
            {
                RuleFor(request => request.Query!.FileName)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .Must(fileName => fileName.StartsWith("./UserUploads/"))
                    .WithMessage("fileName must start with '.%2FUserUploads%2F'");
            });
        }
    }
}
