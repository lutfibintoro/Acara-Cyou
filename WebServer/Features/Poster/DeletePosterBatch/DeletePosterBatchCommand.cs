using FluentResults;
using MediatR;

namespace WebServer.Features.Poster.DeletePosterBatch
{
    public record DeletePosterBatchCommand
    (
        Guid UserId,
        List<Guid> PosterIds
    ) : IRequest<Result>;
}
