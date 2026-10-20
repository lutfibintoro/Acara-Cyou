using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebServer.Infrastructures.Data.Entities
{
    public class AcaraCyouUser : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ConcurrencyCheck]
        public int RowVersion { get; set; } = 1;


        public ICollection<Event> Events { get; set; } = [];
        public ICollection<Order> Orders { get; set; } = [];
    }
}
