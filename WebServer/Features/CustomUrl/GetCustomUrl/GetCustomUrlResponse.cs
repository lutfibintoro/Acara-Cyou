namespace WebServer.Features.CustomUrl.GetCustomUrl
{
    public class GetCustomUrlResponse
    {
        public Guid EventId { get; set; }
        public string FormCustomUrl { get; set; } = string.Empty;
        public string PageCustomUrl { get; set; } = string.Empty;
    }
}
