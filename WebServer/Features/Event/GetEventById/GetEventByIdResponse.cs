namespace WebServer.Features.Event.GetEventById
{
    public class GetEventByIdResponse
    {
        public Guid EventId { get; set; }
        public Guid AcaraCyouUserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FormTemplate { get; set; } = string.Empty;
        public bool IsRegistrationOpen { get; set; }
        public DateTime OpenRegistrationDate { get; set; }
        public DateTime CloseRegistrationDate { get; set; }
        public string BankAccountNumber { get; set; } = string.Empty;
        public string BankAccountName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RowVersion { get; set; }
        public List<Poster> Posters { get; set; } = [];
    }

    public class Poster
    {
        public Guid PosterId { get; set; }
        public string FullPathUrl { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public ulong ByteLength { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RowVersion { get; set; }
    }
}
