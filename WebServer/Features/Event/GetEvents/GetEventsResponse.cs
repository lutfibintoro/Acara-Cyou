namespace WebServer.Features.Event.GetEvents
{
    public class GetEventsResponse
    {
        public Guid EventId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Poster> Posters { get; set; } = [];
    }

    public class Poster
    {
        public string FullPathUrl { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public ulong ByteLength { get; set; }
    }
}
