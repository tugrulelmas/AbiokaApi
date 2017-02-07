using AbiokaApi.ApplicationService.DTOs;
using AbiokaApi.ApplicationService.Implementations;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Helper;
using Moq;

namespace AbiokaApi.UnitTest.Service.Mock
{
    class UserServiceMock : UserService
    {
        public readonly Mock<IUserRepository> RepositoryMock;
        public readonly Mock<IUserSecurityRepository> UserSecurityRepositoryMock;
        public readonly Mock<IAbiokaToken> AbiokaTokenMock;
        public readonly Mock<ICurrentContext> CurrentContextMock;

        public UserServiceMock(Mock<IUserRepository> repository, Mock<IUserSecurityRepository> userSecurityRepository, Mock<IRoleRepository> roleRepository,
                               Mock<IAbiokaToken> abiokaToken, Mock<ICurrentContext> currentContext, Mock<IDTOMapper> dtoMapper)
            : base(repository.Object, userSecurityRepository.Object, roleRepository.Object, abiokaToken.Object, currentContext.Object, dtoMapper.Object) {
            RepositoryMock = repository;
            UserSecurityRepositoryMock = userSecurityRepository;
            AbiokaTokenMock = abiokaToken;
            CurrentContextMock = currentContext;
        }

        public static UserServiceMock Create() => new UserServiceMock(new Mock<IUserRepository>(), new Mock<IUserSecurityRepository>(), new Mock<IRoleRepository>(), new Mock<IAbiokaToken>(), new Mock<ICurrentContext>(), new Mock<IDTOMapper>());
    }
}
