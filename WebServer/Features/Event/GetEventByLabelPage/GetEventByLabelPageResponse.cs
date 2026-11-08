namespace WebServer.Features.Event.GetEventByLabelPage
{
    public class GetEventByLabelPageResponse
    {
        public Guid EventId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AfterBuyDescription { get; set; } = string.Empty;
        public string FormTemplate { get; set; } = string.Empty;
        public string FormCustomUrl { get; set; } = Guid.NewGuid().ToString();
        public string PageCustomUrl { get; set; } = Guid.NewGuid().ToString();
        public bool IsRegistrationOpen { get; set; } = false;
        public DateTime OpenRegistrationDate { get; set; } = DateTime.UtcNow;
        public DateTime CloseRegistrationDate { get; set; } = DateTime.UtcNow + TimeSpan.FromMinutes(1);
        public string BankAccountNumber { get; set; } = string.Empty;
        public string BankAccountName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public List<GetEventByLabelPagePoster> Posters { get; set; } = [];
    }

    public class GetEventByLabelPagePoster
    {
        public Guid PosterId { get; set; }
        public string FullPathUrl { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public ulong ByteLength { get; set; }
    }
}
