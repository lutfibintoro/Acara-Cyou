using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.Poster.DeletePosterBatch
{
    public class DeletePosterBatchRequestValidator : Validator<DeletePosterBatchRequest>
    {
        public DeletePosterBatchRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.Query)
                .NotNull();

            When(request => request.Query is not null, () =>
            {
                RuleFor(request => request.Query!.PosterIds)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .NotEmpty();
            });
        }
    }
}
