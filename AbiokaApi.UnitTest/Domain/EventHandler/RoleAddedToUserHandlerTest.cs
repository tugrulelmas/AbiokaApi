using AbiokaApi.Domain;
using AbiokaApi.Domain.Events;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.UnitTest.Domain.EventHandler.Mock;
using Moq;
using NUnit.Framework;
using System;

namespace AbiokaApi.UnitTest.Domain.EventHandler
{
    [TestFixture]
    public class RoleAddedToUserHandlerTest
    {
        [Test]
        public void Handle_Calls_AddToUser_Method() {
            var roleAddedToUserHandler = RoleAddedToUserHandlerMock.Create();
            var roleAddedToUser = new RoleAddedToUser(User.Empty(Guid.NewGuid()), Guid.NewGuid());

            roleAddedToUserHandler.Handle(roleAddedToUser);

            roleAddedToUserHandler.RoleRepositoryMock.Verify(r => r.AddToUser(roleAddedToUser.RoleId, roleAddedToUser.User.Id), Times.Once());
        }
    }
}
