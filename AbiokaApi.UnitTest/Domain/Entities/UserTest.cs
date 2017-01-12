using AbiokaApi.Domain;
using AbiokaApi.Domain.Events;
using AbiokaApi.Infrastructure.Common.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaApi.UnitTest.Domain.Entities
{
    [TestFixture]
    public class UserTest
    {
        [Test]
        public void AddRole_Adds_Role() {
            var user = User.Empty(Guid.NewGuid());
            var role = new Role(Guid.NewGuid(), "test");
            user.AddRole(role);
            var addedRole = user.Roles.FirstOrDefault(r => r.Id == role.Id);

            Assert.IsNotNull(addedRole);
            Assert.AreEqual(role.Id, addedRole.Id);
            Assert.AreEqual(role.Name, addedRole.Name);
            Assert.AreEqual(1, user.Roles.Count());
        }

        [Test]
        public void AddRole_Adds_RoleAddedToUser_Event() {
            var user = User.Empty(Guid.NewGuid());
            var role = new Role(Guid.NewGuid(), "test");
            user.AddRole(role);
            var roleAddedToUser = (RoleAddedToUser)user.Events.FirstOrDefault(e => e.GetType() == typeof(RoleAddedToUser));

            Assert.IsNotNull(roleAddedToUser);
            Assert.AreEqual(role.Id, roleAddedToUser.RoleId);
            Assert.AreEqual(user.Id, roleAddedToUser.User.Id);
            Assert.AreEqual(1, user.Events.Count());
        }

        [Test]
        public void AddRole_Throws_An_Exception_If_The_Role_Is_Already_Added() {
            var user = User.Empty(Guid.NewGuid());
            var role = new Role(Guid.NewGuid(), "test");
            user.AddRole(role);

            var exception = Assert.Throws<DenialException>(() => user.AddRole(role));

            Assert.IsNotNull(exception);
            Assert.AreEqual(1, user.Events.Count());
        }

        [Test]
        public void RemoveRole_Removes_Role() {
            var user = User.Empty(Guid.NewGuid());
            var role = new Role(Guid.NewGuid(), "test");
            user.AddRole(new Role(Guid.NewGuid(), "test 2"));
            user.AddRole(role);
            user.AddRole(new Role(Guid.NewGuid(), "test 3"));
            user.RemoveRole(role);
            var removedRole = user.Roles.FirstOrDefault(r => r.Id == role.Id);

            Assert.IsNull(removedRole);
            Assert.AreEqual(2, user.Roles.Count());
        }

        [Test]
        public void RemoveRole_Adds_RoleRemovedFromUser_Event() {
            var user = User.Empty(Guid.NewGuid());
            var role = new Role(Guid.NewGuid(), "test");
            user.AddRole(role);
            user.RemoveRole(role);
            var roleRemovedFromUser = (RoleRemovedFromUser)user.Events.FirstOrDefault(e => e.GetType() == typeof(RoleRemovedFromUser));

            Assert.IsNotNull(roleRemovedFromUser);
            Assert.AreEqual(role.Id, roleRemovedFromUser.RoleId);
            Assert.AreEqual(user.Id, roleRemovedFromUser.User.Id);
            Assert.AreEqual(2, user.Events.Count());
        }

        [Test]
        public void RemoveRole_Throws_An_Exception_If_The_Role_Is_Not_Added_Before() {
            var user = User.Empty(Guid.NewGuid());
            var role = new Role(Guid.NewGuid(), "test");

            var exception = Assert.Throws<DenialException>(() => user.RemoveRole(role));

            Assert.IsNotNull(exception);
            Assert.AreEqual(0, user.Events.Count());
        }

        [Test]
        public void Check_Event_Counts() {
            var user = User.Empty(Guid.NewGuid());
            var role = new Role(Guid.NewGuid(), "test");
            user.AddRole(new Role(Guid.NewGuid(), "test 2"));
            user.AddRole(role);
            user.AddRole(new Role(Guid.NewGuid(), "test 3"));
            user.RemoveRole(role);
            user.AddRole(new Role(Guid.NewGuid(), "test 3"));

            Assert.AreEqual(5, user.Events.Count());
        }

        [Test]
        public void SetRoles_Removes_All_Roles() {
            var user = new User(Guid.NewGuid(), string.Empty, new List<Role>() { new Role(Guid.NewGuid(), "test"), new Role(Guid.NewGuid(), "test") });

            user.SetRoles(null);

            Assert.AreEqual(0, user.Roles.Count());
        }

        [Test]
        public void SetRoles_Adds_All_Roles() {
            var user = User.Empty(Guid.NewGuid());

            user.SetRoles(new List<Role>() { new Role(Guid.NewGuid(), "test"), new Role(Guid.NewGuid(), "test") });

            Assert.AreEqual(2, user.Roles.Count());
        }

        [Test]
        public void SetRoles_Adds_And_Removes_All_Roles() {
            var role1 = new Role(Guid.NewGuid(), "test 1");
            var role2 = new Role(Guid.NewGuid(), "test 2");
            var role3 = new Role(Guid.NewGuid(), "test 3");
            var role4 = new Role(Guid.NewGuid(), "test 4");

            var user = new User(Guid.NewGuid(), string.Empty, new List<Role>() { role1, role2, role3 });

            user.SetRoles(new List<Role>() { role2, role4 });

            Assert.AreEqual(2, user.Roles.Count());
            Assert.IsTrue(user.Roles.Any(r => r.Id == role2.Id));
            Assert.IsTrue(user.Roles.Any(r => r.Id == role4.Id));
        }

        [Test]
        public void Clear_Events_On_Initialize_For_Existing_User() {
            var user = new User(Guid.NewGuid(), string.Empty, new List<Role>() { new Role(Guid.NewGuid(), "test"), new Role(Guid.NewGuid(), "test") });

            Assert.AreEqual(0, user.Events.Count());
        }

        [Test]
        public void Not_Clear_Events_On_Initialize_For_New_User() {
            var user = new User(Guid.Empty, string.Empty, new List<Role>() { new Role(Guid.NewGuid(), "test"), new Role(Guid.NewGuid(), "test") });

            Assert.AreEqual(2, user.Events.Count());
        }
    }
}
