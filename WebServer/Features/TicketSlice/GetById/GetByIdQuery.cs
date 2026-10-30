using FluentResults;
using MediatR;

namespace WebServer.Features.TicketSlice.GetById
{
    public class GetByIdQuery : IRequest<Result<GetByIdResponse>>
    {
        public Guid TicketId { get; set; }
    }
}
