using FastEndpoints;

namespace WebServer.Features.Event.UpdateEventById
{
    public record UpdateEventByIdRequest
    (
        Guid EventId,
        [property: FromBody]
        Body Body
    );

    public record Body
    (
        string Title,
        string Description,
        string AfterBuyDescription,
        string FormTemplate,
        bool IsRegistrationOpen,
        DateTime OpenRegistrationDate,
        DateTime CloseRegistrationDate,
        string BankAccountNumber,
        string BankAccountName,
        bool IsDeleted
    );
}
