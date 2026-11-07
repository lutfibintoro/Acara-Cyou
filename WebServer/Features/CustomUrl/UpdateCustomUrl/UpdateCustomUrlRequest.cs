using System.ComponentModel.DataAnnotations;
using FastEndpoints;

namespace WebServer.Features.CustomUrl.UpdateCustomUrl
{
    public class UpdateCustomUrlRequest
    {
        [FromBody, Required]
        public Body Body { get; set; } = null!;
    }

    public class Body
    {
        [Required]
        public Guid EventId { get; set; }
        [Required]
        public string FormCustomUrl { get; set; } = string.Empty;
        [Required]
        public string PageCustomUrl { get; set; } = string.Empty;
    }
}
