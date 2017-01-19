using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using AbiokaApi.UnitTest.Service.Mock;
using NUnit.Framework;
using System;
using System.Net;

namespace AbiokaApi.UnitTest.Service
{
    [TestFixture]
    public class AddUserValidationTest
    {
        [Test]
        public void Add_Throws_User_Is_Already_Registered() {
            var registerUserRequest = new RegisterUserRequest {
                Email = "test@abioka.com",
                Password = "1234"
            };

            var userService = AddUserRequestValidatorMock.Create();
            userService.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(registerUserRequest.Email)).Returns(UserSecurity.CreateBasic(Guid.Empty, string.Empty, string.Empty));
            var exception = Assert.Throws<DenialException>(() => userService.DataValidate(registerUserRequest, ActionType.Add));

            Assert.AreEqual(exception.Message, "UserIsAlreadyRegistered");
            Assert.AreEqual(exception.StatusCode, HttpStatusCode.BadRequest);
        }
    }
}
