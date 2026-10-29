using FastEndpoints;

namespace WebServer.Features.TicketSlice.TicketsDashboard
{
    public class TicketsDashboardSummary : Summary<TicketsDashboardEndpoint, TicketsDashboardRequest>
    {
        public TicketsDashboardSummary()
        {
            Response<List<TicketsDashboardResponse>>(StatusCodes.Status200OK, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status403Forbidden, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
