using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.UnitTest.Service.Mock;
using Moq;
using NUnit.Framework;
using System;

namespace AbiokaApi.UnitTest.Service
{
    [TestFixture]
    public class UserServiceTest
    {
        [Test]
        public void Login_Set_Token() {
            var password = "1234";

            var userSecurity = UserSecurity.CreateBasic(
                Guid.Empty,
                "test@abioka.com",
                password
            );
            userSecurity.Id = Guid.NewGuid();

            var userService = UserServiceMock.Create();
            userService.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(userSecurity.Email)).Returns(userSecurity);
            var token = Guid.NewGuid().ToString();
            userService.AbiokaTokenMock.Setup(t => t.Encode(It.Is<UserClaim>(uc => uc.Email == userSecurity.Email && uc.Id == userSecurity.Id))).Returns(token);
            userService.Login(new LoginRequest {
                Email = userSecurity.Email,
                Password = password
            });

            Assert.AreEqual(userSecurity.Token, token);
            userService.UserSecurityRepositoryMock.Verify(us => us.Update(userSecurity), Times.Once());
        }

        [Test]
        public void Add_Calls_Repository_Add_Method_And_Returns_User() {
            var addUserRequest = new AddUserRequest {
                Email = "test@abioka.com",
                Password = "1234"
            };

            var userService = UserServiceMock.Create();
            userService.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(addUserRequest.Email)).Returns((UserSecurity)null);
            userService.CurrentContextMock.Setup(ct => ct.Principal).Returns(new CustomPrincipal("") { Language = "en" });
            userService.CurrentContextMock.Setup(ct => ct.Current).Returns(userService.CurrentContextMock.Object);
            var user = userService.Add(addUserRequest);

            var userSecurity = UserSecurity.CreateBasic(Guid.Empty, addUserRequest.Email, addUserRequest.Password);

            userService.UserSecurityRepositoryMock.Verify(us => us.Add(It.Is<UserSecurity>(e => e.Email == addUserRequest.Email && e.AuthProvider == AuthProvider.Local && e.Password == userSecurity.Password && e.Language == "en")), Times.Once());
            Assert.AreEqual(user.Email, addUserRequest.Email);
        }
    }
}
