using System.Net;
using System.Security.Claims;
using FastEndpoints;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using WebServer.Extensions;

namespace WebServer.Features.GoogleLogin.Redirect
{
    public class RedirectEndpoint : Ep.Req<RedirectRequest>.NoRes
    {
        private readonly ILogger<RedirectEndpoint> _logger;
        private readonly ISender _sender;
        private readonly string _callbackPage;

        public RedirectEndpoint(ILogger<RedirectEndpoint> logger, ISender sender, IConfiguration configuration)
        {
            _logger = logger;
            _sender = sender;
            _callbackPage = configuration["Authentication:Google:CallbackPage"]!;
        }

        public override void Configure()
        {
            Get("/api/google-login/redirect");
            AuthSchemes(ApplicationAuth.ConstantAuthScheme.ExternalAuthenticationCookie);
            Options(builder => builder.ExcludeFromDescription());
        }

        public override async Task HandleAsync(RedirectRequest req, CancellationToken ct)
        {
            var command = new RedirectCommand
            (
                NameIdentifier: User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).First().Value,
                Name: User.Claims.Where(claim => claim.Type == ClaimTypes.Name).First().Value,
                Email: User.Claims.Where(claim => claim.Type == ClaimTypes.Email).First().Value,
                LoginProvider: User.Identity?.AuthenticationType ?? "Unknown",
                ProfileImageUrl: User.FindFirstValue("urn:google:picture") ?? "/"
            );

            Result<string> commandResult = await _sender.Send(command, ct);
            await HttpContext.SignOutAsync(ApplicationAuth.ConstantAuthScheme.ExternalAuthenticationCookie);

            RedirectHttpResult result = TypedResults.Redirect($"{_callbackPage}?returnUrl={WebUtility.UrlEncode(req.Query!.ReturnUrl)}&key={commandResult.Value}", false, false);
            await Send.ResultAsync(result);
            return;
        }
    }
}
