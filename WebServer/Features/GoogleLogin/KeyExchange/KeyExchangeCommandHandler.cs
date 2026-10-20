using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using WebServer.Infrastructures.Data;
using WebServer.Infrastructures.Data.Entities;
using WebServer.Infrastructures.ResultError;

namespace WebServer.Features.GoogleLogin.KeyExchange
{
    public class KeyExchangeCommandHandler : IRequestHandler<KeyExchangeCommand, Result<KeyExchangeResponse>>
    {
        private readonly ILogger<KeyExchangeCommandHandler> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly AcaraCyouDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public KeyExchangeCommandHandler(ILogger<KeyExchangeCommandHandler> logger, IMemoryCache memoryCache, AcaraCyouDbContext dbContext, IConfiguration configuration)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<Result<KeyExchangeResponse>> Handle(KeyExchangeCommand request, CancellationToken cancellationToken)
        {
            Result<UserInformation> changeKeyResult = ChangeKey(request.Key);
            if (changeKeyResult.IsFailed)
                return changeKeyResult.ToResult<KeyExchangeResponse>();

            var isExist = await IsUserAlreadyExistsAsync(changeKeyResult.Value);
            if (isExist)
                await ConfirmUserLoginProviderAsync(changeKeyResult.Value);
            else
                await CreateUserWithLoginProviderAsync(changeKeyResult.Value);

            Result<(string jwt, string refreshToken)> tokenResult = await CreateTokenAsync(changeKeyResult.Value);

            if (tokenResult.IsFailed)
                return tokenResult.ToResult<KeyExchangeResponse>();

            return Result.Ok(new KeyExchangeResponse(tokenResult.Value.jwt, tokenResult.Value.refreshToken, changeKeyResult.Value.ProfileImageUrl));
        }

        #region exchange key
        private Result<UserInformation> ChangeKey(string key)
        {
            string? userInformationSrting = _memoryCache.Get<string>(key);
            if (string.IsNullOrEmpty(userInformationSrting) || string.IsNullOrWhiteSpace(userInformationSrting))
            {
                var notFound = new NotFoundError<string>("key", key, null, Severity.Error);
                return Result.Fail<UserInformation>(notFound);
            }
            _memoryCache.Remove(key);

            UserInformation userInformation = JsonSerializer.Deserialize<UserInformation>(userInformationSrting)!;
            return Result.Ok<UserInformation>(userInformation);
        }
        #endregion

        #region confirm user
        private async Task<bool> IsUserAlreadyExistsAsync(UserInformation userInformation)
        {
            AcaraCyouUser? user = await _dbContext.AcaraCyouUser
                .Where(acaraCyouUser => acaraCyouUser.Email == userInformation.Email)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user is null)
                return false;
            return true;
        }

        private async Task CreateUserWithLoginProviderAsync(UserInformation userInformation)
        {
            var user = new AcaraCyouUser()
            {
                Email = userInformation.Email,
                UserName = userInformation.Name,
                NormalizedUserName = userInformation.Name.ToLower()
            };
            await _dbContext.AddAsync(user);

            var userLogin = new AcaraCyouUserLogin()
            {
                LoginProvider = userInformation.LoginProvider,
                ProviderKey = userInformation.NameIdentifier,
                ProviderDisplayName = userInformation.LoginProvider,
                UserId = user.Id
            };
            await _dbContext.AddAsync(userLogin);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException) { }
        }

        private async Task ConfirmUserLoginProviderAsync(UserInformation userInformation)
        {
            AcaraCyouUser? user = await _dbContext.AcaraCyouUser
                .Where(acaraCyouUser => acaraCyouUser.Email == userInformation.Email)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            AcaraCyouUserLogin? userLogin = await _dbContext.AcaraCyouUserLogin
                .Where(acaraCyouUserLogin => acaraCyouUserLogin.UserId == user!.Id)
                .Where(acaraCyouUserLogin => acaraCyouUserLogin.ProviderKey == userInformation.NameIdentifier)
                .Where(acaraCyouUserLogin => acaraCyouUserLogin.LoginProvider == userInformation.LoginProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (userLogin is null)
            {
                userLogin = new AcaraCyouUserLogin()
                {
                    LoginProvider = userInformation.LoginProvider,
                    ProviderKey = userInformation.NameIdentifier,
                    ProviderDisplayName = userInformation.LoginProvider,
                    UserId = user!.Id
                };
                await _dbContext.AddAsync(userLogin);

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException) { }
            }
        }
        #endregion

        #region generate token
        private async Task<Result<(string jwt, string refreshToken)>> CreateTokenAsync(UserInformation userInformation)
        {
            AcaraCyouUser? user = await _dbContext.AcaraCyouUser
                .Where(acaraCyouUser => acaraCyouUser.Email == userInformation.Email)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            Result<string> refreshToken = await GenerateAndSaveRefreshToken(user!);
            Result<string> jwt = GenerateJwt(user!);

            return Result.Ok<(string jwt, string refreshToken)>((jwt.Value, refreshToken.Value));
        }

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

        private async Task<Result<string>> GenerateAndSaveRefreshToken(AcaraCyouUser acaraCyouUser)
        {
            string refreshToken = Base64Url.EncodeToString(Guid.NewGuid().ToByteArray());

            AcaraCyouUserToken? userToken = await _dbContext.AcaraCyouUserToken
                .Where(acaraCyouUserToken => acaraCyouUserToken.Name == "refresh-token")
                .Where(acaraCyouUserToken => acaraCyouUserToken.LoginProvider == "Application")
                .Where(acaraCyouUserToken => acaraCyouUserToken.UserId == acaraCyouUser.Id)
                .FirstOrDefaultAsync();

            if (userToken is null)
            {
                await _dbContext.AcaraCyouUserToken.AddAsync(new AcaraCyouUserToken()
                {
                    CreatedAt = DateTime.UtcNow,
                    ExpiredAt = DateTime.UtcNow.AddDays(7),
                    Name = "refresh-token",
                    Value = refreshToken,
                    UserId = acaraCyouUser.Id,
                    LoginProvider = "Application"
                });

                try
                {
                    await _dbContext.SaveChangesAsync();
                    return Result.Ok<string>(refreshToken);
                }
                catch (DbUpdateException)
                {
                    userToken = await _dbContext.AcaraCyouUserToken
                        .Where(acaraCyouUserToken => acaraCyouUserToken.Name == "refresh-token")
                        .Where(acaraCyouUserToken => acaraCyouUserToken.LoginProvider == "Application")
                        .Where(acaraCyouUserToken => acaraCyouUserToken.UserId == acaraCyouUser.Id)
                        .FirstOrDefaultAsync();
                }
            }

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    userToken!.ExpiredAt = DateTime.UtcNow.AddDays(7);
                    userToken.RowVersion++;

                    await _dbContext.SaveChangesAsync();
                    break;
                }
                catch (DbUpdateConcurrencyException) { }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            return Result.Ok<string>(userToken!.Value!);
        }
        #endregion

        #region support record or class
        public record UserInformation(string NameIdentifier, string Name, string Email, string LoginProvider, string ProfileImageUrl);
        #endregion
    }
}