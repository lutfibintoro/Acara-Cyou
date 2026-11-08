using FluentResults;
using MediatR;

namespace WebServer.Features.Event.GetEventByLabelPage
{
    public class GetEventByLabelPageQuery : IRequest<Result<GetEventByLabelPageResponse>>
    {
        public string CustomUrlName { get; set; } = string.Empty;
    }
}
