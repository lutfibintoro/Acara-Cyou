using FastEndpoints;

namespace WebServer.Features.TicketSlice.AddTicket
{
    public class AddTicketSummary : Summary<AddTicketEndpoint, AddTicketResponse>
    {
        public AddTicketSummary()
        {
            Response<AddTicketResponse>(StatusCodes.Status200OK, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status403Forbidden, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
