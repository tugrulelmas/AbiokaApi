using AbiokaApi.Infrastructure.Common.Exceptions;

namespace AbiokaApi.Infrastructure.Framework.Authentication
{

    public class SignatureVerificationException : ValidationException
    {
        public SignatureVerificationException(string message)
            : base(message) {
        }
    }
}
