using AbiokaApi.ApplicationService.EventHandlers;
using AbiokaApi.Domain.Repositories;
using Moq;

namespace AbiokaApi.UnitTest.Domain.EventHandler.Mock
{
    class RoleRemovedFromUserHandlerMock : RoleRemovedFromUserHandler
    {
        public readonly Mock<IRoleRepository> RoleRepositoryMock;

        public RoleRemovedFromUserHandlerMock(Mock<IRoleRepository> roleRepository)
            : base(roleRepository.Object) {
            RoleRepositoryMock = roleRepository;
        }

        public static RoleRemovedFromUserHandlerMock Create() => new RoleRemovedFromUserHandlerMock(new Mock<IRoleRepository>());
    }
}
