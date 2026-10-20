using System.Net;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebServer.Features.GoogleLogin.Challenge
{
    public class ChallengeEndpoint : Ep.Req<ChallengeRequest>.NoRes
    {
        private readonly ILogger<ChallengeEndpoint> _logger;

        public ChallengeEndpoint(ILogger<ChallengeEndpoint> logger)
        {
            _logger = logger;
        }

        public override void Configure()
        {
            Get("/api/google-login/challenge");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ChallengeRequest req, CancellationToken ct)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/google-login/redirect?returnUrl={WebUtility.UrlEncode(req.Query!.ReturnUrl)}"
            };

            ChallengeHttpResult result = TypedResults.Challenge(properties, [GoogleDefaults.AuthenticationScheme]);
            await Send.ResultAsync(result);
            return;
        }
    }
}
