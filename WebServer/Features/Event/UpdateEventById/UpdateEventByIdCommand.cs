using FluentResults;
using MediatR;

namespace WebServer.Features.Event.UpdateEventById
{
    public class UpdateEventByIdCommand : IRequest<Result>
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string AfterBuyDescription { get; set; } = null!;
        public string FormTemplate { get; set; } = null!;
        public bool IsRegistrationOpen { get; set; }
        public DateTime OpenRegistrationDate { get; set; }
        public DateTime CloseRegistrationDate { get; set; }
        public string BankAccountNumber { get; set; } = null!;
        public string BankAccountName { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
