using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using AbiokaApi.UnitTest.Service.Mock;
using NUnit.Framework;
using System.Net;

namespace AbiokaApi.UnitTest.Service
{
    [TestFixture]
    public class LoginValidationTest
    {
        [Test]
        public void Login_Throws_User_Not_Found() {
            var loginRequest = new LoginRequest {
                Email = "test@abioka.com",
                Password = "1234"
            };
            var loginRequestValidator = LoginRequestValidatorMock.Create();
            loginRequestValidator.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(loginRequest.Email)).Returns((UserSecurity)null);
            var exception = Assert.Throws<DenialException>(() => loginRequestValidator.DataValidate(loginRequest, ActionType.Add));

            Assert.AreEqual(exception.Message, "UserNotFound");
            Assert.AreEqual(exception.StatusCode, HttpStatusCode.NotFound);
        }

        [Test]
        public void Login_Throws_Wrong_Password() {
            var userSecurity = new UserSecurity {
                Email = "test@abioka.com",
                Password = "1234",
            };
            userSecurity.Password = userSecurity.GetHashedPassword(userSecurity.Password);

            var loginRequestValidator = LoginRequestValidatorMock.Create();
            loginRequestValidator.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(userSecurity.Email)).Returns(userSecurity);
            var exception = Assert.Throws<DenialException>(() => loginRequestValidator.DataValidate(new LoginRequest {
                Email = userSecurity.Email,
                Password = "123"
            }, ActionType.Add));

            Assert.AreEqual(exception.Message, "WrongPassword");
            Assert.AreEqual(exception.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Login_Throws_User_IsDeleted() {
            var password = "1234";
            var userSecurity = new UserSecurity {
                Email = "test@abioka.com",
                Password = password,
                IsDeleted = true
            };
            userSecurity.Password = userSecurity.GetHashedPassword(userSecurity.Password);

            var loginRequestValidator = LoginRequestValidatorMock.Create();
            loginRequestValidator.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(userSecurity.Email)).Returns(userSecurity);
            var exception = Assert.Throws<DenialException>(() => loginRequestValidator.DataValidate(new LoginRequest {
                Email = userSecurity.Email,
                Password = password
            }, ActionType.Add));

            Assert.AreEqual(exception.Message, "UserIsNotActive");
            Assert.AreEqual(exception.StatusCode, HttpStatusCode.BadRequest);
        }
    }
}
