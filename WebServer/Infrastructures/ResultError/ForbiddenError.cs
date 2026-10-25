using FluentResults;
using FluentValidation;

namespace WebServer.Infrastructures.ResultError
{
    public class ForbiddenError<TForbidden> : Error
    {
        public ForbiddenError(string fieldName, TForbidden forbiddenValue, string? message, Severity severity) : base($"Value [{forbiddenValue}] is forbidden")
        {
            if (message is not null)
                Message = message;

            Metadata.Add("status", 403);
            Metadata.Add("fieldName", fieldName);
            Metadata.Add("code", "FORBIDDEN_FAILURE");
            Metadata.Add("value", forbiddenValue);
            Metadata.Add("severity", severity);
        }
    }
}
