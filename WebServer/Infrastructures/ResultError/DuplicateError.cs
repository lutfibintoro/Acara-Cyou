using FluentResults;
using FluentValidation;

namespace WebServer.Infrastructures.ResultError
{
    public class DuplicateError<TDuplicated> : Error
    {
        public DuplicateError(string fieldName, TDuplicated duplicatedValue, string? message, Severity severity) : base($"Value [{duplicatedValue}] alread exist")
        {
            if (message is not null)
                Message = message;

            Metadata.Add("status", 409);
            Metadata.Add("fieldName", fieldName);
            Metadata.Add("code", "DUPLICATE_RESOURCE");
            Metadata.Add("value", duplicatedValue);
            Metadata.Add("severity", severity);
        }
    }
}
