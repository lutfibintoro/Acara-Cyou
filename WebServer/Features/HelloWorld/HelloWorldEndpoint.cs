using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebServer.Features.HelloWorld
{
    public class HelloWorldEndpoint : Ep.NoReq.Res<string>
    {
        private readonly ILogger<HelloWorldEndpoint> _logger;


        public HelloWorldEndpoint(ILogger<HelloWorldEndpoint> logger)
        {
            _logger = logger;
        }


        public override void Configure()
        {
            Get("/api/hello-world");
            AllowAnonymous();
        }


        public override async Task HandleAsync(CancellationToken ct)
        {
            Ok<string> result = TypedResults.Ok("hello-world");
            await Send.ResultAsync(result);
            return;
        }
    }
}
