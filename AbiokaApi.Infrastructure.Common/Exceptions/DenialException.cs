using System.Net;

namespace AbiokaApi.Infrastructure.Common.Exceptions
{
    public class DenialException : ApiException
    {
        public DenialException(string errorCode)
            : this(HttpStatusCode.BadRequest, errorCode) {
        }

        public DenialException(HttpStatusCode statusCode, string errorCode)
            : base(errorCode ) {
            ContentValue = errorCode;
            StatusCode = statusCode;
            ExtraHeaders.Add("Status-Reason", "validation-failed");
        }
    }
}