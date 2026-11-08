using FastEndpoints;

namespace WebServer.Features.Event.GetEventByLabelForm
{
    public class GetEventByLabelFormSummary : Summary<GetEventByLabelFormEndpoint, GetEventByLabelFormRequest>
    {
        public GetEventByLabelFormSummary()
        {
            Response<GetEventByLabelFormResponse>(StatusCodes.Status200OK, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
