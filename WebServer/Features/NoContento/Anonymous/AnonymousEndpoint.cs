using FastEndpoints;

namespace WebServer.Features.NoContento.Anonymous
{
    public class AnonymousEndpoint : Ep.NoReq.NoRes
    {
        private readonly ILogger<AnonymousEndpoint> _logger;

        public AnonymousEndpoint(ILogger<AnonymousEndpoint> logger)
        {
            _logger = logger;
        }

        public override void Configure()
        {
            Get("/api/no-content");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = TypedResults.NoContent();
            await Send.ResultAsync(result);
            return;
        }
    }
}
