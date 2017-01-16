using AbiokaApi.ApplicationService.Validation;
using FluentValidation;

namespace AbiokaApi.ApplicationService.Messaging
{
    public class ChangePasswordRequest : ServiceRequestBase
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }

    public class ChangePasswordRequestValidator : CustomValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator() {

            RuleFor(cp => cp.OldPassword).NotEmpty().WithMessage("IsRequired");
            RuleFor(cp => cp.NewPassword).NotEmpty().WithMessage("IsRequired");
        }
    }
}
