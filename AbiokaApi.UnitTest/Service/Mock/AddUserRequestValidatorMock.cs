using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Helper;
using Moq;

namespace AbiokaApi.UnitTest.Service.Mock
{
    class AddUserRequestValidatorMock : AddUserRequestValidator
    {
        public readonly Mock<IUserSecurityRepository> UserSecurityRepositoryMock;

        public AddUserRequestValidatorMock(Mock<IUserSecurityRepository> userSecurityRepository)
            : base(userSecurityRepository.Object) {
            UserSecurityRepositoryMock = userSecurityRepository;
        }

        public new void DataValidate(RegisterUserRequest instance, ActionType actionType) {
            base.DataValidate(instance, actionType);
        }

        public static AddUserRequestValidatorMock Create() => new AddUserRequestValidatorMock(new Mock<IUserSecurityRepository>());
    }
}
