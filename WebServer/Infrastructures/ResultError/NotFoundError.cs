using FluentResults;

namespace WebServer.Infrastructures.ResultError
{
    public class NotFoundError<TNotFound> : Error
    {
        public NotFoundError(string fieldName, TNotFound notFoundValue, string? message, Severity severity) : base($"Item [{notFoundValue}] was not found")
        {
            if (message is not null)
                Message = message;

            Metadata.Add("status", 404);
            Metadata.Add("fieldName", fieldName);
            Metadata.Add("code", "NOT_FOUND_FAILURE");
            Metadata.Add("value", notFoundValue);
            Metadata.Add("severity", severity);
        }
    }
}
