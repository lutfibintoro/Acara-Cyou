using FluentResults;
using MediatR;

namespace WebServer.Features.CustomUrl.GetCustomUrl
{
    public record GetCustomUrlQuery() : IRequest<Result<List<GetCustomUrlResponse>>>;
}
