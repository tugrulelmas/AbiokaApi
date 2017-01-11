using AbiokaApi.ApplicationService.Implementations;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Domain;
using Moq;

namespace AbiokaApi.UnitTest.Service.Mock
{
    class UserServiceMock : UserService
    {
        public readonly Mock<IUserRepository> RepositoryMock;
        public readonly Mock<IUserSecurityRepository> UserSecurityRepositoryMock;
        public readonly Mock<IAbiokaToken> AbiokaTokenMock;

        public UserServiceMock(Mock<IUserRepository> repository, Mock<IUserSecurityRepository> userSecurityRepository, Mock<IAbiokaToken> abiokaToken, Mock<IEventDispatcher> eventDispatcher)
            : base(repository.Object, userSecurityRepository.Object, abiokaToken.Object, eventDispatcher.Object) {
            RepositoryMock = repository;
            UserSecurityRepositoryMock = userSecurityRepository;
            AbiokaTokenMock = abiokaToken;
        }

        public static UserServiceMock Create() => new UserServiceMock(new Mock<IUserRepository>(), new Mock<IUserSecurityRepository>(), new Mock<IAbiokaToken>(), new Mock<IEventDispatcher>());
    }
}
