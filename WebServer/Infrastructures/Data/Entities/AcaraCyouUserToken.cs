using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebServer.Infrastructures.Data.Entities
{
    public class AcaraCyouUserToken : IdentityUserToken<Guid>
    {
        public Guid Id { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ConcurrencyCheck]
        public int RowVersion { get; set; } = 1;
    }
}
