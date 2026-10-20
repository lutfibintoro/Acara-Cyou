using FluentResults;

namespace WebServer.Infrastructures.ResultError
{
    public class ValidationError<TNotValid> : Error
    {
        public ValidationError(string fieldName, TNotValid notValidValue, string? message, Severity severity) : base($"validation failed, fix and try again later")
        {
            if (message is not null)
                Message = message;

            Metadata.Add("status", 400);
            Metadata.Add("fieldName", fieldName);
            Metadata.Add("code", "VALIDATION_FAILURE");
            Metadata.Add("value", notValidValue);
            Metadata.Add("severity", severity);
        }
    }
}
