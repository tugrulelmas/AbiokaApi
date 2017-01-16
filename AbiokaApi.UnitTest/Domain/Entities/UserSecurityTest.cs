using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Exceptions;
using NUnit.Framework;
using System;

namespace AbiokaApi.UnitTest.Domain.Entities
{
    [TestFixture]
    public class UserSecurityTest
    {
        [Test]
        public void ArePasswordEqual() {
            var userSecurity = UserSecurity.CreateBasic(Guid.Empty, "test@abioka.com", "1234");

            Assert.IsTrue(userSecurity.ArePasswordEqual(userSecurity.Email, "1234"));
            Assert.IsFalse(userSecurity.ArePasswordEqual(userSecurity.Email, "1235"));
        }

        [Test]
        public void ComputeHashPassword() {
            var userSecurity = UserSecurity.CreateBasic(Guid.Empty, "test@abioka.com", "1234");

            Assert.AreNotEqual("1234", userSecurity.Password);
        }

        [Test]
        public void NotComputeHashPassword() {
            var userSecurity = UserSecurity.CreateBasic(Guid.NewGuid(), "test@abioka.com", "1234");

            Assert.AreEqual("1234", userSecurity.Password);
        }

        [Test]
        public void ChangePassword_Throws_AnException_If_The_NewPassword_Is_Null() {
            var password = "1234";
            var userSecurity = UserSecurity.CreateBasic(Guid.NewGuid(), "test@abioka.com", password);

            var exception = Assert.Throws<DenialException>(() => userSecurity.ChangePassword(password, string.Empty));

            Assert.IsNotNull(exception);
            Assert.AreEqual("PasswordCannotBeEmpty", exception.Message);
        }

        [Test]
        public void ChangePassword_Throws_AnException_If_The_OldPassword_Is_Wrong() {
            var userSecurity = UserSecurity.CreateBasic(Guid.NewGuid(), "test@abioka.com", "1234");

            var exception = Assert.Throws<DenialException>(() => userSecurity.ChangePassword("123", "1234"));

            Assert.IsNotNull(exception);
            Assert.AreEqual("WrongPassword", exception.Message);
        }

        [Test]
        public void ChangePassword_Throws_AnException_If_The_OldPassword_And_NewPassword_Are_Same() {
            var password = "1234";
            var userSecurity = UserSecurity.CreateBasic(Guid.Empty, "test@abioka.com", password);

            var exception = Assert.Throws<DenialException>(() => userSecurity.ChangePassword(password, password));

            Assert.IsNotNull(exception);
            Assert.AreEqual("NewPasswordCannotBeSameAsTheOldPassword", exception.Message);
        }

        [Test]
        public void ChangePassword_Changes_Password_And_RefreshToken() {
            var password = "1234";
            var newPassword = "12345";
            var refreshToken = Guid.NewGuid().ToString();
            var userSecurity = new UserSecurity(Guid.Empty, "test@abioka.com", AuthProvider.Local, string.Empty, refreshToken, string.Empty, password, string.Empty, false, null);

            userSecurity.ChangePassword(password, newPassword);

            Assert.IsTrue(userSecurity.ArePasswordEqual(userSecurity.Email, newPassword));
            Assert.AreNotEqual(refreshToken, userSecurity.Token);
        }
    }
}
