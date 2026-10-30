using FastEndpoints;
using FluentValidation;

namespace WebServer.Features.TicketSlice.GetById
{
    public class GetByIdRequestValidator : Validator<GetByIdRequest>
    {
        public GetByIdRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(request => request.TicketId)
                .NotNull();
        }
    }
}
