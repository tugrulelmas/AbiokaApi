using AbiokaApi.ApplicationService.Validation;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using FluentValidation;
using System;
using System.Net;

namespace AbiokaApi.ApplicationService.Messaging
{
    public class LoginRequest : ServiceRequestBase
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

    public class LoginRequestValidator : CustomValidator<LoginRequest>
    {
        private readonly IUserSecurityRepository userSecurityRepository;
        private readonly ILoginAttemptRepository loginAttemptRepository;

        public LoginRequestValidator(IUserSecurityRepository userSecurityRepository, ILoginAttemptRepository loginAttemptRepository) {
            this.userSecurityRepository = userSecurityRepository;
            this.loginAttemptRepository = loginAttemptRepository;

            RuleFor(r => r.Email).NotEmpty().WithMessage("IsRequired").EmailAddress().WithMessage("ShouldBeCorrectEmail");
            RuleFor(lr => lr.Password).NotEmpty().WithMessage("IsRequired");
        }

        protected override void DataValidate(LoginRequest instance, ActionType actionType) {
            var user = userSecurityRepository.GetByEmail(instance.Email);

            if (user == null) {
                throw new DenialException(HttpStatusCode.NotFound, "UserNotFound");
            }

            var hashedPassword = user.GetHashedPassword(instance.Password);

            var loginAttempt = new LoginAttempt {
                Date = DateTime.UtcNow,
                Token = user.Token,
                User = user,
                IP = "ada"
            };

            if (user.Password != hashedPassword) {
                loginAttempt.LoginResult = LoginResult.WrongPassword;
                loginAttemptRepository.Add(loginAttempt);

                throw new DenialException("WrongPassword");
            }

            if (user.IsDeleted) {
                loginAttempt.LoginResult = LoginResult.UserIsNotActive;
                loginAttemptRepository.Add(loginAttempt);

                throw new DenialException("UserIsNotActive");
            }

            loginAttempt.LoginResult = LoginResult.Successfull;
            loginAttemptRepository.Add(loginAttempt);
        }
    }
}
