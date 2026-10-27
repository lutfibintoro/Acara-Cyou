using FluentResults;
using MediatR;

namespace WebServer.Features.Event.AddEvent
{
    public class AddEventCommand : IRequest<Result>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Poster>? Posters { get; set; }
    }
}
