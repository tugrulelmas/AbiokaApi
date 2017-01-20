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
    public class LocalAuthServiceTest
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

            var localAuthService = LocalAuthServiceMock.Create();
            localAuthService.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(userSecurity.Email)).Returns(userSecurity);
            var token = Guid.NewGuid().ToString();
            localAuthService.AbiokaTokenMock.Setup(t => t.Encode(It.Is<UserClaim>(uc => uc.Email == userSecurity.Email && uc.Id == userSecurity.Id))).Returns(token);
            var result = localAuthService.LoginAsync(new AuthRequest {
                Email = userSecurity.Email,
                Password = password
            }).Result;

            Assert.AreEqual(userSecurity.Token, token);
            localAuthService.UserSecurityRepositoryMock.Verify(us => us.Update(userSecurity), Times.Once());
        }
    }
}