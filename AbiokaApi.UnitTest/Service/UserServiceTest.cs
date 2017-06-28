using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Events;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.UnitTest.Service.Mock;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace AbiokaApi.UnitTest.Service
{
    [TestFixture]
    public class UserServiceTest
    {
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

        [Test]
        public void Delete_Adds_UserIsDeleted_Event_And_Calls_Repository_Delete_Method() {
            var user = User.Empty(Guid.NewGuid(), "test@abioka.com");

            var userService = UserServiceMock.Create();
            userService.RepositoryMock.Setup(us => us.FindById(user.Id)).Returns(user);

            userService.Delete(user.Id);

            userService.RepositoryMock.Verify(us => us.Delete(user), Times.Once());
            Assert.AreEqual(1, user.Events.Count(e => e.GetType() == typeof(UserIsDeleted)));
            var userIsDeleted = (UserIsDeleted)user.Events.First(e => e.GetType() == typeof(UserIsDeleted));
            Assert.AreEqual(user.Id, userIsDeleted.UserId);
            Assert.AreEqual(user.Email, userIsDeleted.Email);
        }
    }
}
