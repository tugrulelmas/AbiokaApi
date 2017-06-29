using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.ApplicationSettings;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly string apiKey;
        private readonly string domain;
        private readonly string from;
        private readonly ICurrentContext currentContext;
        private readonly IExceptionLogRepository exceptionLogRepository;

        public EmailService(IConnectionStringRepository connectionStringRepository, ICurrentContext currentContext, IExceptionLogRepository exceptionLogRepository) {
            var mailgunApiValues = connectionStringRepository.ReadAppSetting("MailgunApiValues").Split(',');
            apiKey = mailgunApiValues[0];
            domain = mailgunApiValues[1];
            from = mailgunApiValues[2];

            this.currentContext = currentContext;
            this.exceptionLogRepository = exceptionLogRepository;
        }

        public async Task SendAsync(EmailRequest emailRequest) {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", $"api:{apiKey}".EncodeWithBase64());

            var form = new Dictionary<string, string>();
            form["from"] = emailRequest.From ?? from;
            form["to"] = emailRequest.To;
            form["cc"] = emailRequest.Cc;
            form["bcc"] = emailRequest.Bcc;
            form["subject"] = emailRequest.Subject;
            form["html"] = emailRequest.Body;

            var response = await client.PostAsync($"https://api.mailgun.net/v3/{domain}/messages", new FormUrlEncodedContent(form));

            if (!response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                var exceptionLog = new ExceptionLog("MailChimpSubscriber", $"To: {emailRequest.To}, Subject: {emailRequest.Subject}", "Mailgun", "Mailgun", content, currentContext.Current.Principal?.Id ?? Guid.Empty, currentContext.Current.IP);
                exceptionLogRepository.Add(exceptionLog);
            }
        }
    }
}
