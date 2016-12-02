namespace AbiokaApi.Infrastructure.Common.Exceptions
{
    public class ValidationException : ApiException
    {
        public ValidationException(string errorCode)
            : base(errorCode) {
            ContentValue = errorCode;
            ExtraHeaders.Add("Status-Reason", "validation-failed");
        }
    }
}