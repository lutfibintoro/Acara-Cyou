using FluentResults;
using FluentValidation;

namespace WebServer.Infrastructures.ResultError
{
    public class ConcurrencyError<TConflicted> : Error
    {
        public ConcurrencyError(string fieldName, TConflicted conflictedValue, string? message, Severity severity) : base($"Value [{conflictedValue}] was concurrently modified")
        {
            if (message is not null)
                Message = message;

            Metadata.Add("status", 409);
            Metadata.Add("fieldName", fieldName);
            Metadata.Add("code", "CONCURRENCY_CONFLICT");
            Metadata.Add("value", conflictedValue);
            Metadata.Add("severity", severity);
        }
    }
}
