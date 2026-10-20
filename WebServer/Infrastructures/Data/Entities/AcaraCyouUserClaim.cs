using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebServer.Infrastructures.Data.Entities
{
    public class AcaraCyouUserClaim : IdentityUserClaim<Guid>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ConcurrencyCheck]
        public int RowVersion { get; set; } = 1;
    }
}
