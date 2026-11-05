using FluentResults;
using MediatR;

namespace WebServer.Features.Paymentso.GetMethod
{
    public record GetMethodQuery() : IRequest<Result<GetMethodResponse>>;
}
