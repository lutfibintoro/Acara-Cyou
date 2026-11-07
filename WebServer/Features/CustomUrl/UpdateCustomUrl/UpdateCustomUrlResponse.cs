using System.ComponentModel.DataAnnotations;

namespace WebServer.Features.CustomUrl.UpdateCustomUrl
{
    public class UpdateCustomUrlResponse
    {
        public Guid EventId { get; set; }
        public string FormCustomUrl { get; set; } = string.Empty;
        public string PageCustomUrl { get; set; } = string.Empty;
    }
}
