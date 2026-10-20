using FastEndpoints;

namespace WebServer.Features.GoogleLogin.Challenge
{
    public class ChallengeSummary : Summary<ChallengeEndpoint, ChallengeRequest>
    {
        public ChallengeSummary()
        {
            Response(StatusCodes.Status204NoContent);
            Response(StatusCodes.Status302Found);
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
