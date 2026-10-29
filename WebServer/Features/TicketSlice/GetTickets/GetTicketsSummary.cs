using FastEndpoints;

namespace WebServer.Features.TicketSlice.GetTickets
{
    public class GetTicketsSummary : Summary<GetTicketsEndpoint, GetTicketsRequest>
    {
        public GetTicketsSummary()
        {
            Response<List<GetTicketsResponse>>(StatusCodes.Status200OK, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
