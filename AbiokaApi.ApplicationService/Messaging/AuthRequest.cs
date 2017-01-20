using AbiokaApi.ApplicationService.Validation;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using FluentValidation;
using System;
using System.Net;

namespace AbiokaApi.ApplicationService.Messaging
{
    public class AuthRequest : ServiceRequestBase
    {
        public string code { get; set; }

        public string clientId { get; set; }

        public string redirectUri { get; set; }

        public AuthProvider provider { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class AuthRequestValidator : CustomValidator<AuthRequest>
    {
        private readonly IUserSecurityRepository userSecurityRepository;
        private readonly ILoginAttemptRepository loginAttemptRepository;
        private readonly ICurrentContext currentContext;

        public AuthRequestValidator(IUserSecurityRepository userSecurityRepository, ILoginAttemptRepository loginAttemptRepository, ICurrentContext currentContext) {
            this.userSecurityRepository = userSecurityRepository;
            this.loginAttemptRepository = loginAttemptRepository;
            this.currentContext = currentContext;

            RuleFor(r => r.code).NotEmpty().WithMessage("IsRequired").When(r => r.provider != AuthProvider.Local);
            RuleFor(r => r.clientId).NotEmpty().WithMessage("IsRequired").When(r => r.provider != AuthProvider.Local);
            RuleFor(r => r.redirectUri).NotEmpty().WithMessage("IsRequired").When(r => r.provider != AuthProvider.Local);
            RuleFor(r => r.provider).NotEmpty().WithMessage("IsRequired").When(r => r.provider != AuthProvider.Local);

            RuleFor(r => r.Email).NotEmpty().WithMessage("IsRequired").When(r => r.provider == AuthProvider.Local);
            RuleFor(r => r.Password).NotEmpty().WithMessage("IsRequired").When(r => r.provider == AuthProvider.Local);
        }

        protected override void DataValidate(AuthRequest instance, ActionType actionType) {
            if (instance.provider != AuthProvider.Local)
                return;

            var user = userSecurityRepository.GetByEmail(instance.Email);

            if (user == null) {
                throw new DenialException(HttpStatusCode.NotFound, "UserNotFound");
            }

            var loginAttempt = new LoginAttempt {
                Date = DateTime.UtcNow,
                Token = user.Token,
                User = user,
                IP = currentContext.Current.IP
            };

            if (!user.ArePasswordEqual(instance.Email, instance.Password)) {
                loginAttempt.LoginResult = LoginResult.WrongPassword;
                loginAttemptRepository.Add(loginAttempt);

                throw new DenialException("WrongPassword");
            }

            if (user.IsDeleted) {
                loginAttempt.LoginResult = LoginResult.UserIsNotActive;
                loginAttemptRepository.Add(loginAttempt);

                throw new DenialException("UserIsNotActive");
            }

            loginAttempt.LoginResult = LoginResult.Successful;
            loginAttemptRepository.Add(loginAttempt);
        }
    }
}
