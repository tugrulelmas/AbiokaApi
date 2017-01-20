using AbiokaApi.ApplicationService.Authentication;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Helper;
using Moq;

namespace AbiokaApi.UnitTest.Service.Mock
{
    class LocalAuthServiceMock : LocalAuthService
    {
        public readonly Mock<IUserRepository> RepositoryMock;
        public readonly Mock<IUserSecurityRepository> UserSecurityRepositoryMock;
        public readonly Mock<IAbiokaToken> AbiokaTokenMock;
        public readonly Mock<ICurrentContext> CurrentContextMock;

        public LocalAuthServiceMock(Mock<IUserSecurityRepository> userSecurityRepository, Mock<IAbiokaToken> abiokaToken)
            : base(userSecurityRepository.Object, abiokaToken.Object) {
            UserSecurityRepositoryMock = userSecurityRepository;
            AbiokaTokenMock = abiokaToken;
        }

        public static LocalAuthServiceMock Create() => new LocalAuthServiceMock(new Mock<IUserSecurityRepository>(), new Mock<IAbiokaToken>());
    }
}
