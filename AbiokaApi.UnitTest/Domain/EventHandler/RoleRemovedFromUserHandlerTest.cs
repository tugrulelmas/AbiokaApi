using AbiokaApi.Domain;
using AbiokaApi.Domain.Events;
using AbiokaApi.UnitTest.Domain.EventHandler.Mock;
using Moq;
using NUnit.Framework;
using System;

namespace AbiokaApi.UnitTest.Domain.EventHandler
{
    [TestFixture]
    public class RoleRemovedFromUserHandlerTest
    {
        [Test]
        public void Handle_Calls_RemoveFromUser_Method() {
            var roleRemovedFromUserHandler = RoleRemovedFromUserHandlerMock.Create();
            var roleRemovedFromUser = new RoleRemovedFromUser(User.Empty(Guid.NewGuid()), Guid.NewGuid());

            roleRemovedFromUserHandler.Handle(roleRemovedFromUser);

            roleRemovedFromUserHandler.RoleRepositoryMock.Verify(r => r.RemoveFromUser(roleRemovedFromUser.RoleId, roleRemovedFromUser.User.Id), Times.Once());
        }
    }
}
