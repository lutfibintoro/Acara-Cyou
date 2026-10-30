using System.ComponentModel.DataAnnotations;

namespace WebServer.Features.TicketSlice.AddTicket
{
    public class AddTicketResponse
    {
        [Required]
        public Guid TicketId { get; set; }
    }
}
