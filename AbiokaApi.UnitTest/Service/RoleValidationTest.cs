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
    public class RoleValidationTest
    {
        private Role role;
        private RoleValidatorMock roleValidator;

        [SetUp]
        public void Initialize() {
            role = new Role {
                Name = "Test"
            };

            roleValidator = RoleValidatorMock.Create();
        }

        [Test]
        public void Throws_Role_Is_Already_Registered_On_Add() {
            roleValidator.RoleRepositoryMock.Setup(us => us.GetByName(role.Name)).Returns(new Role { Id = Guid.NewGuid() });
            var exception = Assert.Throws<DenialException>(() => roleValidator.DataValidate(role, ActionType.Add));

            Assert.AreEqual(exception.Message, "RoleIsAlreadyRegistered");
            Assert.AreEqual(exception.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Throws_Role_Is_Already_Registered_On_Update() {
            roleValidator.RoleRepositoryMock.Setup(us => us.GetByName(role.Name)).Returns(new Role { Id = Guid.NewGuid() });
            var exception = Assert.Throws<DenialException>(() => roleValidator.DataValidate(role, ActionType.Update));

            Assert.AreEqual(exception.Message, "RoleIsAlreadyRegistered");
            Assert.AreEqual(exception.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Does_Not_Throw_Role_Is_Already_Registered_On_Update_Itself() {
            roleValidator.RoleRepositoryMock.Setup(us => us.GetByName(role.Name)).Returns(new Role { Id = role.Id });

            Assert.DoesNotThrow(() => roleValidator.DataValidate(role, ActionType.Update));
        }
    }
}
