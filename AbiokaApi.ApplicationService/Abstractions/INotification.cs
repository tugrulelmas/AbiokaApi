using AbiokaApi.ApplicationService.Messaging;

namespace AbiokaApi.ApplicationService.Abstractions
{
    public interface INotificationService : IService
    {
        /// <summary>
        /// Sends the verification email.
        /// </summary>
        /// <param name="request">The request.</param>
        void SendVerificationEmail(SendVerificationEmailRequest request);
    }
}
