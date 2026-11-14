using FluentResults;
using FluentValidation;

namespace WebServer.Infrastructures.ResultError
{
    public class ThirdPartError<TErrorValue> : Error
    {
        public ThirdPartError(string fieldName, TErrorValue thirdPartErrorValue, string? message, Severity severity) : base($"api [{thirdPartErrorValue}] was an error")
        {
            if (message is not null)
                Message = message;

            Metadata.Add("status", 503);
            Metadata.Add("fieldName", fieldName);
            Metadata.Add("code", "THIRD_API_ERROR");
            Metadata.Add("value", thirdPartErrorValue);
            Metadata.Add("severity", severity);
        }
    }
}
