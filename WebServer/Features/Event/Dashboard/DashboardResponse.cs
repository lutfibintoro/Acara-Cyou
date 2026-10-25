namespace WebServer.Features.Event.Dashboard
{
    public class DashboardResponse
    {
        public decimal TotalNetRevenue { get; set; }
        public List<TicketStatistic> Tickets { get; set; } = [];
        public int TotalSuccessOrder { get; set; }
        public int TotalPendingOrder { get; set; }
        public List<DaySuccessRegistration> DailySuccessRegistrations { get; set; } = [];
    }

    public class TicketStatistic
    {
        public string Category { get; set; } = string.Empty;
        public int Quota { get; set; }
        public int BookedQuota { get; set; }
    }

    public class DaySuccessRegistration
    {
        public string Date { get; set; } = string.Empty;
        public int TotalSuccess { get; set; }
    }
}
