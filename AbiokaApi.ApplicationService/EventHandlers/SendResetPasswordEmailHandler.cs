using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Events;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Helper;
using System.Threading.Tasks;

namespace AbiokaApi.ApplicationService.EventHandlers
{
    public class SendResetPasswordEmailHandler : IEventHandler<PasswordIsReset>
    {
        private readonly IEmailService emailService;
        private readonly ITemplateReader templateReader;

        public SendResetPasswordEmailHandler(IEmailService emailService, ITemplateReader templateReader) {
            this.emailService = emailService;
            this.templateReader = templateReader;
        }

        public int Order => 5;

        public void Handle(PasswordIsReset eventInstance) {
            var user = eventInstance.User as UserSecurity;
            if (user == null)
                return;

            var template = templateReader.ReadTemplate(new ReadTemplateRequest {
                Key = "ResetPasswordEmailTemplate",
                Language = user.Language
            });

            var bodyText = template.Body.Replace("{{Name}}", user.Name).Replace("{{Surname}}", user.Surname).Replace("{{Url}}", user.ProviderToken.EncodeWithBase64());

            var emailRequest = new EmailRequest {
                To = user.Email,
                Subject = template.Subject,
                Body = bodyText
            };

            Task.Run(async () => await emailService.SendAsync(emailRequest));
        }
    }
}
