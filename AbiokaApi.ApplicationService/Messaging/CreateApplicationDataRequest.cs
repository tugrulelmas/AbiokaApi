using AbiokaApi.ApplicationService.Validation;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using FluentValidation;

namespace AbiokaApi.ApplicationService.Messaging
{
    public class CreateApplicationDataRequest : ServiceRequestBase
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
    }

    public class CreateApplicationDataRequestValidator : CustomValidator<CreateApplicationDataRequest>
    {
        private readonly IUserRepository userRepository;

        public CreateApplicationDataRequestValidator(IUserRepository userRepository) {
            this.userRepository = userRepository;

            RuleFor(r => r.Email).NotEmpty().WithMessage("IsRequired").EmailAddress().WithMessage("ShouldBeCorrectEmail");
            RuleFor(r => r.Password).NotEmpty().WithMessage("IsRequired");
        }

        protected override void DataValidate(CreateApplicationDataRequest instance, ActionType actionType) {
            var count = userRepository.Count();
            if (count > 0)
                throw new DenialException("DBIsNotEmpty");
        }
    }
}
