﻿using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Events;
using AbiokaApi.Infrastructure.Common.Domain;
using System.Threading.Tasks;

namespace AbiokaApi.ApplicationService.EventHandlers
{
    public class SendWelcomeEmailHandler : IEventHandler<EmailIsVerified>
    {
        private readonly IEmailService emailService;
        private readonly ITemplateReader templateReader;

        public SendWelcomeEmailHandler(IEmailService emailService, ITemplateReader templateReader) {
            this.emailService = emailService;
            this.templateReader = templateReader;
        }

        public int Order => 5;

        public void Handle(EmailIsVerified eventInstance) {
            var user = eventInstance.User as UserSecurity;
            if (user == null || !user.IsEmailVerified)
                return;

            var template = templateReader.ReadTemplate(new ReadTemplateRequest {
                Key = "WelcomeEmailTemplate",
                Language = user.Language
            });

            var bodyText = template.Body.Replace("{{Name}}", user.Name).Replace("{{Surname}}", user.Surname);

            var emailRequest = new EmailRequest {
                To = user.Email,
                Subject = template.Subject,
                Body = bodyText
            };

            Task.Run(async () => await emailService.SendAsync(emailRequest));
        }
    }
}
