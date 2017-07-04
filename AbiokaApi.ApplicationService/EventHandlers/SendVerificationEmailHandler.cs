using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Events;
using AbiokaApi.Infrastructure.Common.Domain;

namespace AbiokaApi.ApplicationService.EventHandlers
{
    public class SendVerificationEmailHandler : IEventHandler<UserIsAdded>
    {
        private readonly INotificationService notificationService;

        public SendVerificationEmailHandler(INotificationService notificationService) {
            this.notificationService = notificationService;
        }

        public int Order => 5;

        public void Handle(UserIsAdded eventInstance) {
            var user = eventInstance.User as UserSecurity;
            if (user == null || user.IsEmailVerified)
                return;

            notificationService.SendVerificationEmail(new SendVerificationEmailRequest { Email = user.Email });
        }
    }
}
