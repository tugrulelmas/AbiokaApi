using AbiokaApi.Domain;
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
    }
}
