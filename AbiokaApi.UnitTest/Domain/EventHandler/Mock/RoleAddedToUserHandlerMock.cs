using AbiokaApi.ApplicationService.EventHandlers;
using AbiokaApi.Domain.Repositories;
using Moq;

namespace AbiokaApi.UnitTest.Domain.EventHandler.Mock
{
    class RoleAddedToUserHandlerMock : RoleAddedToUserHandler
    {
        public readonly Mock<IRoleRepository> RoleRepositoryMock;

        public RoleAddedToUserHandlerMock(Mock<IRoleRepository> roleRepository)
            : base(roleRepository.Object) {
            RoleRepositoryMock = roleRepository;
        }

        public static RoleAddedToUserHandlerMock Create() => new RoleAddedToUserHandlerMock(new Mock<IRoleRepository>());
    }
}
