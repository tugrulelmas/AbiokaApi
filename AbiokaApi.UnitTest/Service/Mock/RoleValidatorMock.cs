using AbiokaApi.ApplicationService.Validation;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Helper;
using Moq;

namespace AbiokaApi.UnitTest.Service.Mock
{
    class RoleValidatorMock : RoleValidator
    {
        public readonly Mock<IRoleRepository> RoleRepositoryMock;

        public RoleValidatorMock(Mock<IRoleRepository> roleRepository)
            : base(roleRepository.Object) {
            RoleRepositoryMock = roleRepository;
        }

        public new void DataValidate(Role instance, ActionType actionType) {
            base.DataValidate(instance, actionType);
        }

        public static RoleValidatorMock Create() => new RoleValidatorMock(new Mock<IRoleRepository>());
    }
}
