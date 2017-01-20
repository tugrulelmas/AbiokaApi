using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Helper;
using Moq;

namespace AbiokaApi.UnitTest.Service.Mock
{
    class AuthRequestValidatorMock : AuthRequestValidator
    {
        public readonly Mock<IUserSecurityRepository> UserSecurityRepositoryMock;
        public readonly Mock<ICurrentContext> CurrentContextMock;
        public readonly Mock<ILoginAttemptRepository> LoginAttemptRepositoryMock;

        public AuthRequestValidatorMock(Mock<IUserSecurityRepository> userSecurityRepository, Mock<ILoginAttemptRepository> loginAttemptRepository, Mock<ICurrentContext> currentContext)
            : base(userSecurityRepository.Object, loginAttemptRepository.Object, currentContext.Object) {
            UserSecurityRepositoryMock = userSecurityRepository;
            CurrentContextMock = currentContext;
            LoginAttemptRepositoryMock = loginAttemptRepository;
        }

        public new void DataValidate(AuthRequest instance, ActionType actionType) {
            base.DataValidate(instance, actionType);
        }

        public static AuthRequestValidatorMock Create() => new AuthRequestValidatorMock(new Mock<IUserSecurityRepository>(), new Mock<ILoginAttemptRepository>(), new Mock<ICurrentContext>());
    }
}
