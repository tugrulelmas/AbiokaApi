using AbiokaApi.ApplicationService.DTOs;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using FluentValidation;

namespace AbiokaApi.ApplicationService.Validation
{
    public class RoleValidator : CustomValidator<RoleDTO>
    {
        private readonly IRoleRepository repository;

        public RoleValidator(IRoleRepository repository) {
            this.repository = repository;

            RuleFor(r => r.Name).NotEmpty().WithMessage("IsRequired");
        }

        protected override void DataValidate(RoleDTO instance, ActionType actionType) {
            if (actionType == ActionType.Add || actionType == ActionType.Update) {
                var role = repository.GetByName(instance.Name);
                if (role != null && role.Id != instance.Id)
                    throw new DenialException("RoleIsAlreadyRegistered", instance.Name);
            }
        }
    }
}
