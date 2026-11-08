using FluentResults;
using MediatR;

namespace WebServer.Features.Event.GetEventByLabelForm
{
    public class GetEventByLabelFormQuery : IRequest<Result<GetEventByLabelFormResponse>>
    {
        public string CustomUrlName { get; set; } = string.Empty;
    }
}
