using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using AbiokaApi.UnitTest.Service.Mock;
using Moq;
using NUnit.Framework;
using System;
using System.Net;

namespace AbiokaApi.UnitTest.Service
{
    [TestFixture]
    public class LoginValidationTest
    {
        AuthRequestValidatorMock authRequestValidator;

        [SetUp]
        public void Init() {
            authRequestValidator = AuthRequestValidatorMock.Create();
            authRequestValidator.CurrentContextMock.Setup(c => c.IP).Returns("127.0.0.0");
            authRequestValidator.CurrentContextMock.Setup(c => c.Current).Returns(authRequestValidator.CurrentContextMock.Object);
        }

        [Test]
        public void Login_Throws_User_Not_Found() {
            var loginRequest = new AuthRequest {
                Email = "test@abioka.com",
                Password = "1234",
                provider = AuthProvider.Local
            };
            authRequestValidator.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(loginRequest.Email)).Returns((UserSecurity)null);
            var exception = Assert.Throws<DenialException>(() => authRequestValidator.DataValidate(loginRequest, ActionType.Add));

            Assert.AreEqual(exception.Message, "UserNotFound");
            Assert.AreEqual(exception.StatusCode, HttpStatusCode.NotFound);
            authRequestValidator.LoginAttemptRepositoryMock.Verify(l => l.Add(It.IsAny<LoginAttempt>()), Times.Never());
        }

        [Test]
        public void Login_Throws_Wrong_Password() {
            var userSecurity = UserSecurity.CreateBasic(Guid.Empty, "test@abioka.com", "1234");

            authRequestValidator.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(userSecurity.Email)).Returns(userSecurity);

            var exception = Assert.Throws<DenialException>(() => authRequestValidator.DataValidate(new AuthRequest {
                Email = userSecurity.Email,
                Password = "123",
                provider = AuthProvider.Local
            }, ActionType.Add));

            Assert.AreEqual(exception.Message, "WrongPassword");
            Assert.AreEqual(exception.StatusCode, HttpStatusCode.BadRequest);
            authRequestValidator.LoginAttemptRepositoryMock.Verify(l => l.Add(It.Is<LoginAttempt>(la => la.LoginResult == LoginResult.WrongPassword)), Times.Once());
        }

        [Test]
        public void Login_Throws_User_IsDeleted() {
            var password = "1234";
            var userSecurity = UserSecurity.CreateBasic(Guid.Empty, "test@abioka.com", password, true, true);

            authRequestValidator.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(userSecurity.Email)).Returns(userSecurity);

            var exception = Assert.Throws<DenialException>(() => authRequestValidator.DataValidate(new AuthRequest {
                Email = userSecurity.Email,
                Password = password,
                provider = AuthProvider.Local
            }, ActionType.Add));

            Assert.AreEqual(exception.Message, "UserIsNotActive");
            Assert.AreEqual(exception.StatusCode, HttpStatusCode.BadRequest);
            authRequestValidator.LoginAttemptRepositoryMock.Verify(l => l.Add(It.Is<LoginAttempt>(la => la.LoginResult == LoginResult.UserIsNotActive)), Times.Once());
        }

        [Test]
        public void Login_Adds_Successful_LoginAttempt() {
            var password = "1234";
            var userSecurity = UserSecurity.CreateBasic(Guid.Empty, "test@abioka.com", password, true, false);

            authRequestValidator.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(userSecurity.Email)).Returns(userSecurity);

            authRequestValidator.DataValidate(new AuthRequest {
                Email = userSecurity.Email,
                Password = password,
                provider = AuthProvider.Local
            }, ActionType.Add);

            authRequestValidator.LoginAttemptRepositoryMock.Verify(l => l.Add(It.Is<LoginAttempt>(la => la.LoginResult == LoginResult.Successful)), Times.Once());
        }
    }
}
