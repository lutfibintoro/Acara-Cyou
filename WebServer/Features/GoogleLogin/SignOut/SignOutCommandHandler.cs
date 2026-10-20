using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.Data.Entities;

namespace WebServer.Features.GoogleLogin.SignOut
{
    public class SignOutCommandHandler : IRequestHandler<SignOutCommand, Result>
    {
        private readonly ILogger<SignOutCommandHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;

        public SignOutCommandHandler(ILogger<SignOutCommandHandler> logger, AcaraCyouDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.AcaraCyouUserToken
                .Where(acaraCyouUserToken => acaraCyouUserToken.Value == request.RefreshToken)
                .ExecuteDeleteAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
