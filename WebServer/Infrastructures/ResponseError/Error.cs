using System.ComponentModel;

namespace WebServer.Infrastructures.ResponseError
{
    /// <summary>
    /// the error details object.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// the name of the error or property of the dto that caused the error.
        /// </summary>
        public virtual string Name { get; set; } = string.Empty;

        /// <summary>
        /// the reason for the error.
        /// </summary>
        public virtual string Reason { get; set; } = string.Empty;

        /// <summary>
        /// the code of the error.
        /// </summary>
        [Description("""
            the code of the error.<br>
            <code><b>OneOf</b>: ["VALIDATION_FAILURE", "DUPLICATE_RESOURCE", "FEATURE_DISABLED", "NOT_FOUND_FAILURE", IDENTITY_FAILURE]</code>
            """)]
        public virtual string Code { get; set; } = string.Empty;

        /// <summary>
        /// the severity of the error.
        /// </summary>
        [Description("""
            the severity of the error.<br>
            <code><b>OneOf</b>: ["Error", "Warning", "Info"]</code>
            """)]
        public virtual string Severity { get; set; } = string.Empty;
    }
}
