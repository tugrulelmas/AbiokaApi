using AbiokaApi.Infrastructure.Common.Exceptions;

namespace AbiokaApi.Infrastructure.Framework.Authentication
{

    public class SignatureVerificationException : DenialException
    {
        public SignatureVerificationException(string message)
            : base(message) {
        }
    }
}
