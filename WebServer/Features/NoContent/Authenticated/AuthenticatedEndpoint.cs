using FastEndpoints;

namespace WebServer.Features.NoContent.Authenticated
{
    public class AuthenticatedEndpoint : Ep.NoReq.NoRes
    {
        private readonly ILogger<AuthenticatedEndpoint> _logger;

        public AuthenticatedEndpoint(ILogger<AuthenticatedEndpoint> logger)
        {
            _logger = logger;
        }

        public override void Configure()
        {
            Get("/api/no-content/authenticated");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = TypedResults.NoContent();
            await Send.ResultAsync(result);
            return;
        }
    }
}
