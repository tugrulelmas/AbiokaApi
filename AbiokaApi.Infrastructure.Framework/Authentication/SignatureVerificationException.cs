using AbiokaApi.Infrastructure.Common.Exceptions;
using System.Net;

namespace AbiokaApi.Infrastructure.Framework.Authentication
{

    public class SignatureVerificationException : DenialException
    {
        public SignatureVerificationException(string message)
            : base(HttpStatusCode.Unauthorized, message) {
        }
    }
}
