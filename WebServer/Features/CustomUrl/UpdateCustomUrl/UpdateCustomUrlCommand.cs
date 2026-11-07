using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace WebServer.Features.CustomUrl.UpdateCustomUrl
{
    public class UpdateCustomUrlCommand : IRequest<Result<UpdateCustomUrlResponse>>
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public string FormCustomUrl { get; set; } = string.Empty;
        public string PageCustomUrl { get; set; } = string.Empty;
    }
}
