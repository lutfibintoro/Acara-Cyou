using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.Data.Entities;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.GoogleLogin.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>
    {
        private readonly ILogger<RefreshTokenCommandHandler> _logger;
        private readonly AcaraCyouDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommandHandler(ILogger<RefreshTokenCommandHandler> logger, AcaraCyouDbContext dbContext, IConfiguration configuration)
        {
            _logger = logger;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            AcaraCyouUserToken? userToken = await _dbContext.AcaraCyouUserToken
                .Where(acaraCyouUserToken => acaraCyouUserToken.Name == "refresh-token")
                .Where(acaraCyouUserToken => acaraCyouUserToken.LoginProvider == "Application")
                .Where(acaraCyouUserToken => acaraCyouUserToken.Value == request.RefreshToken)
                .FirstOrDefaultAsync(cancellationToken);

            if (userToken is null)
            {
                var notFound = new NotFoundError<string>("refreshToken", request.RefreshToken, null, Severity.Error);
                return Result.Fail<RefreshTokenResponse>(notFound);
            }

            AcaraCyouUser? user = await _dbContext.AcaraCyouUser
                .Where(usr => usr.Id == userToken.UserId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                var notFound = new NotFoundError<string>("refreshTokenUsr", request.RefreshToken, null, Severity.Error);
                return Result.Fail<RefreshTokenResponse>(notFound);
            }

            Result<string> jwtResult = GenerateJwt(user);

            Result saveResult = await UpdateTokenAsync(userToken, request);
            if (saveResult.IsFailed)
                return saveResult.ToResult<RefreshTokenResponse>();

            return Result.Ok<RefreshTokenResponse>(new RefreshTokenResponse(jwtResult.Value));
        }

        #region update refresh token
        private async Task<Result> UpdateTokenAsync(AcaraCyouUserToken userToken, RefreshTokenCommand request)
        {
            for (int i = 0; i < 10; i++)
            {
                userToken.ExpiredAt = DateTime.UtcNow.AddDays(7);
                userToken.RowVersion++;
                try
                {
                    await _dbContext.SaveChangesAsync();
                    return Result.Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await Task.Delay(200);
                    await _dbContext.Entry(userToken).ReloadAsync();
                }
            }

            return Result.Fail(new ValidationError<string>("refreshToken", request.RefreshToken, null, Severity.Error));
        }
        #endregion

        #region jwt generator
        private Result<string> GenerateJwt(AcaraCyouUser acaraCyouUser)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Name, acaraCyouUser.UserName!),
                new Claim(ClaimTypes.Email, acaraCyouUser.Email!),
                new Claim(ClaimTypes.NameIdentifier, acaraCyouUser.Id.ToString())
            ];
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Authentication:Token")!));
            var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Authentication:Issuer"),
                audience: _configuration.GetValue<string>("Authentication:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );
            string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return Result.Ok<string>(jwt);
        }
        #endregion
    }
}
