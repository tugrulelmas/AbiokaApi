using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Helper;
using Moq;

namespace AbiokaApi.UnitTest.Service.Mock
{
    class LoginRequestValidatorMock : LoginRequestValidator
    {
        public readonly Mock<IUserSecurityRepository> UserSecurityRepositoryMock;

        public LoginRequestValidatorMock(Mock<IUserSecurityRepository> userSecurityRepository, Mock<ILoginAttemptRepository> loginAttemptRepository)
            : base(userSecurityRepository.Object, loginAttemptRepository.Object) {
            UserSecurityRepositoryMock = userSecurityRepository;
        }

        public new void DataValidate(LoginRequest instance, ActionType actionType) {
            base.DataValidate(instance, actionType);
        }

        public static LoginRequestValidatorMock Create() => new LoginRequestValidatorMock(new Mock<IUserSecurityRepository>(), new Mock<ILoginAttemptRepository>());
    }
}
