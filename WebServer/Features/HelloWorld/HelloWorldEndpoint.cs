using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebServer.Features.HelloWorld
{
    public class HelloWorldEndpoint : Ep.Req<HelloWorldRequest>.Res<string>
    {
        private readonly ILogger<HelloWorldEndpoint> _logger;


        public HelloWorldEndpoint(ILogger<HelloWorldEndpoint> logger)
        {
            _logger = logger;
        }


        public override void Configure()
        {
            Post("/api/hello-world");
            AllowAnonymous();
        }


        public override async Task HandleAsync(HelloWorldRequest req, CancellationToken ct)
        {
            Ok<string> result = TypedResults.Ok("hello-world: " + req.Body.OptionList.ToString());
            await Send.ResultAsync(result);
            return;
        }
    }
}
