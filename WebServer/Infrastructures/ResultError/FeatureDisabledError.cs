using FluentResults;

namespace WebServer.Infrastructures.ResultError
{
    public class FeatureDisabledError : Error
    {
        public FeatureDisabledError(string fieldName, string? message, Severity severity) : base($"features is no longer accessible")
        {
            if (message is not null)
                Message = message;

            Metadata.Add("status", 400);
            Metadata.Add("fieldName", fieldName);
            Metadata.Add("code", "FEATURE_DISABLED");
            Metadata.Add("severity", severity);
        }
    }
}
