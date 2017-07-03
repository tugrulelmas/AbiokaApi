using AbiokaApi.ApplicationService.Validation;
using FluentValidation;

namespace AbiokaApi.ApplicationService.Messaging
{
    public class ReadTemplateRequest : ServiceRequestBase
    {
        public string Key { get; set; }

        public string Language { get; set; }
    }

    public class ReadTemplateRequestValidator : CustomValidator<ReadTemplateRequest>
    {
        public ReadTemplateRequestValidator() {
            RuleFor(r => r.Key).NotEmpty().WithMessage("IsRequired");
        }
    }
}
