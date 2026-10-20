using System.ComponentModel.DataAnnotations;

namespace WebServer.Infrastructures.Data.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FormTemplate { get; set; } = string.Empty;
        public bool IsRegistrationOpen { get; set; } = false;
        public DateTime OpenRegistrationDate { get; set; } = DateTime.UtcNow;
        public DateTime CloseRegistrationDate { get; set; } = DateTime.UtcNow + TimeSpan.FromMinutes(1);
        public string BankAccountNumber { get; set; } = string.Empty;
        public string BankAccountName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ConcurrencyCheck]
        public int RowVersion { get; set; } = 1;


        public Guid AcaraCyouUserId { get; set; }
        public AcaraCyouUser AcaraCyouUser { get; set; } = null!;


        public ICollection<Poster> Posters { get; set; } = [];


        public ICollection<DisbursementDetail> DisbursementDetails { get; set; } = [];


        public ICollection<TicketCategory> TicketCategorys { get; set; } = [];
    }
}
