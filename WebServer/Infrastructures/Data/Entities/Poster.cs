using System.ComponentModel.DataAnnotations;

namespace WebServer.Infrastructures.Data.Entities
{
    public class Poster
    {
        public Guid Id { get; set; }
        public string FullPathUrl { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public ulong ByteLength { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ConcurrencyCheck]
        public int RowVersion { get; set; } = 1;


        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;
    }
}
