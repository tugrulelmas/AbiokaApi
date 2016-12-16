using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using FluentValidation;

namespace AbiokaApi.ApplicationService.Validation
{
    public class RoleValidator : CustomValidator<Role>
    {
        private readonly IRoleRepository repository;

        public RoleValidator(IRoleRepository repository) {
            this.repository = repository;

            RuleFor(r => r.Name).NotEmpty().WithMessage("IsRequired");
        }

        protected override void DataValidate(Role instance, ActionType actionType) {
            if (actionType == ActionType.Add) {
                var role = repository.GetByName(instance.Name);
                if (role != null)
                    throw new DenialException("RoleIsAlreadyRegistered", instance.Name);
            }
        }
    }
}
