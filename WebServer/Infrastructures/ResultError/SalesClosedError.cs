using FluentResults;
using FluentValidation;

namespace WebServer.Infrastructures.ResultError
{
    public class SalesClosedError<TSalesClosed> : Error
    {
        public SalesClosedError(string fieldName, TSalesClosed closedValue, string? message, Severity severity) : base($"sales [{closedValue}] was closed")
        {
            if (message is not null)
                Message = message;

            Metadata.Add("status", 422);
            Metadata.Add("fieldName", fieldName);
            Metadata.Add("code", "ITEM_SALES_CLOSED");
            Metadata.Add("value", closedValue);
            Metadata.Add("severity", severity);
        }
    }
}
