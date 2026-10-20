using System.Text.Json;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace WebServer.Features.GoogleLogin.Redirect
{
    public class RedirectCommandHandler : IRequestHandler<RedirectCommand, Result<string>>
    {
        private readonly ILogger<RedirectCommandHandler> _logger;
        private readonly IMemoryCache _memoryCache;

        public RedirectCommandHandler(ILogger<RedirectCommandHandler> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<Result<string>> Handle(RedirectCommand request, CancellationToken cancellationToken)
        {
            string key = Guid.NewGuid().ToString();

            string userInformation = JsonSerializer.Serialize(request);

            _memoryCache.Set(key, userInformation, TimeSpan.FromSeconds(30));

            return Result.Ok<string>(key);
        }
    }
}
