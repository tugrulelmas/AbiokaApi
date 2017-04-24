using AbiokaApi.ApplicationService.Validation;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using FluentValidation;
using System;
using System.Net;

namespace AbiokaApi.ApplicationService.Messaging
{
    public class ChangePasswordRequest : ServiceRequestBase
    {
        public Guid UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }

    public class ChangePasswordRequestValidator : CustomValidator<ChangePasswordRequest>
    {
        private readonly ICurrentContext currentContext;

        public ChangePasswordRequestValidator(ICurrentContext currentContext) {
            this.currentContext = currentContext;

            RuleFor(cp => cp.OldPassword).NotEmpty().WithMessage("IsRequired");
            RuleFor(cp => cp.NewPassword).NotEmpty().WithMessage("IsRequired");
        }

        protected override void DataValidate(ChangePasswordRequest instance, ActionType actionType) {
            if(currentContext.Current.Principal.Id != instance.UserId) {
                throw new DenialException(HttpStatusCode.Forbidden, "AccessDenied");
            }
        }
    }
}
