using AbiokaApi.ApplicationService.Validation;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using FluentValidation;
using System.Net;

namespace AbiokaApi.ApplicationService.Messaging
{
    public class SendVerificationEmailRequest : ServiceRequestBase
    {
        public string Email { get; set; }
    }

    public class SendVerificationEmailRequestValidator : CustomValidator<SendVerificationEmailRequest>
    {
        private readonly IUserSecurityRepository userSecurityRepository;

        public SendVerificationEmailRequestValidator(IUserSecurityRepository userSecurityRepository) {
            this.userSecurityRepository = userSecurityRepository;

            RuleFor(r => r.Email).NotEmpty().WithMessage("IsRequired").EmailAddress().WithMessage("ShouldBeCorrectEmail");
        }

        protected override void DataValidate(SendVerificationEmailRequest instance, ActionType actionType) {
            var user = userSecurityRepository.GetByEmail(instance.Email);
            if (user == null)
                throw new DenialException(HttpStatusCode.NotFound, "UserNotFound");

            if(user.IsEmailVerified)
                throw new DenialException("AccountIsAlreadyVerified");
        }
    }
}
