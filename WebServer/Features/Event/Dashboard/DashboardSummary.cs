using FastEndpoints;

namespace WebServer.Features.Event.Dashboard
{
    public class DashboardSummary : Summary<DashboardEndpoint, DashboardRequest>
    {
        public DashboardSummary()
        {
            Description = "Returns the event dashboard: total net revenue (grand_total - application_fee) of successful orders, ticket statistics per category, successful and pending order counts, and the daily successful registrations series for the last 10 days. Only the owner of the event can view it.";

            Params["eventId"] = "The unique identifier (guid) of the event to load the dashboard for";

            Response<DashboardResponse>(StatusCodes.Status200OK, contentType: "application/json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status400BadRequest, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status401Unauthorized, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status404NotFound, contentType: "application/problem+json");
            Response<Infrastructures.ResponseError.ProblemDetails>(StatusCodes.Status500InternalServerError, contentType: "application/problem+json");
        }
    }
}
