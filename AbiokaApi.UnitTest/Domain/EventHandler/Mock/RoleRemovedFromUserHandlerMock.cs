using AbiokaApi.Domain.Repositories;
using AbiokaApi.Repository.EventHandlers;
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
