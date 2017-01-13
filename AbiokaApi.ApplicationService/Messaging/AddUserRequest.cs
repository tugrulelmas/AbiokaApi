using AbiokaApi.ApplicationService.DTOs;
using AbiokaApi.ApplicationService.Validation;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using FluentValidation;
using System.Collections.Generic;

namespace AbiokaApi.ApplicationService.Messaging
{
    public class AddUserRequest : ServiceRequestBase
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

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public IEnumerable<RoleDTO> Roles { get; set; }
    }

    public class AddUserRequestValidator : CustomValidator<AddUserRequest>
    {
        private readonly IUserSecurityRepository userSecurityRepository;

        public AddUserRequestValidator(IUserSecurityRepository userSecurityRepository) {
            this.userSecurityRepository = userSecurityRepository;

            RuleFor(r => r.Email).NotEmpty().WithMessage("IsRequired").EmailAddress().WithMessage("ShouldBeCorrectEmail");
            RuleFor(r => r.Password).NotEmpty().WithMessage("IsRequired");
        }

        protected override void DataValidate(AddUserRequest instance, ActionType actionType) {
            var tmpUser = userSecurityRepository.GetByEmail(instance.Email);
            if (tmpUser != null)
                throw new DenialException("UserIsAlreadyRegistered", instance.Email);
        }
    }
}
