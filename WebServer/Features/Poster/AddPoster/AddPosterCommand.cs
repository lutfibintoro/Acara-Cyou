using FluentResults;
using MediatR;

namespace WebServer.Features.Poster.AddPoster
{
    public record AddPosterCommand
    (
        Guid UserId,
        Guid EventId,
        string FullPathUrl,
        string MimeType,
        ulong ByteLength
    ) : IRequest<Result<AddPosterResponse>>;
}
