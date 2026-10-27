using System.ComponentModel.DataAnnotations;
using FastEndpoints;

namespace WebServer.Features.Poster.UpdatePosterById
{
    public class UpdatePosterByIdRequest
    {
        [FromBody]
        public Body Body { get; set; } = null!;
    }

    public class Body
    {
        [Required]
        public Guid PosterId { get; set; }
        public string FullPathUrl { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public ulong ByteLength { get; set; }
    }
}
