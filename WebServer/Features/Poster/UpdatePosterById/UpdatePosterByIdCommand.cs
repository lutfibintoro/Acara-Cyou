using FluentResults;
using MediatR;

namespace WebServer.Features.Poster.UpdatePosterById
{
    public class UpdatePosterByIdCommand : IRequest<Result>
    {
        public Guid UserId { get; set; }
        public Guid PosterId { get; set; }
        public string FullPathUrl { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public ulong ByteLength { get; set; }
    }
}
