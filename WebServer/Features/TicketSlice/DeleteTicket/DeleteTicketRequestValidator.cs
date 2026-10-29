using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.TicketSlice.DeleteTicket
{
    public class DeleteTicketRequestValidator : Validator<DeleteTicketRequest>
    {
        public DeleteTicketRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.TicketId)
                .NotNull();
        }
    }
}
