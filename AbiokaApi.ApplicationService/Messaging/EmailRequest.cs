using AbiokaApi.ApplicationService.Validation;
using FluentValidation;

namespace AbiokaApi.ApplicationService.Messaging
{
    public class EmailRequest : ServiceRequestBase
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }

    public class EmailRequestValidator : CustomValidator<EmailRequest>
    {
        public EmailRequestValidator() {
            RuleFor(r => r.From).EmailAddress().WithMessage("ShouldBeCorrectEmail");
            RuleFor(r => r.To).NotEmpty().WithMessage("IsRequired").EmailAddress().WithMessage("ShouldBeCorrectEmail");
            RuleFor(r => r.Subject).NotEmpty().WithMessage("IsRequired");
            RuleFor(r => r.Body).NotEmpty().WithMessage("IsRequired");
        }
    }
}
