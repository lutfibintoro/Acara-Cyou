using FluentResults;
using MediatR;

namespace WebServer.Features.Event.UpdateEventById
{
    public record UpdateEventByIdCommand
    (
        Guid UserId,
        Guid EventId,
        string Title,
        string Description,
        string FormTemplate,
        bool IsRegistrationOpen,
        DateTime OpenRegistrationDate,
        DateTime CloseRegistrationDate,
        string BankAccountNumber,
        string BankAccountName,
        bool IsDeleted
    ) : IRequest<Result>;
}
