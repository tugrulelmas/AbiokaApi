using AbiokaApi.Domain.Events;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaApi.Domain
{
    public class User : IdEntity<Guid>
    {
        protected readonly List<Role> roles;

        public User(Guid id, string email, IEnumerable<Role> roles) {
            Id = id;
            Email = email;
            this.roles = new List<Role>();

            if (roles.IsNotNullAndEmpty()) {
                AddRoles(roles);
            }

            if (Id.IsNotNullAndEmpty()) {
                ClearEvents();
            }
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; protected set; }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public IEnumerable<Role> Roles => roles;

        /// <summary>
        /// Adds the role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <exception cref="DenialException"></exception>
        public void AddRole(Role role) {
            var tmpRole = roles.FirstOrDefault(r => r.Id == role.Id);
            if (tmpRole != null)
                throw new DenialException($"The user with email {Email} has already a role with id {role.Id}");

            roles.Add(role);
            AddEvent(new RoleAddedToUser(this, role.Id));
        }

        private void AddRoles(IEnumerable<Role> roles) {
            foreach (var roleItem in roles) {
                AddRole(roleItem);
            }
        }

        /// <summary>
        /// Deletes the role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <exception cref="DenialException"></exception>
        public void RemoveRole(Role role) {
            var tmpRole = roles.FirstOrDefault(r => r.Id == role.Id);
            if (tmpRole == null)
                throw new DenialException($"The user with email {Email} has not a role with id {role.Id}");

            roles.Remove(role);
            AddEvent(new RoleRemovedFromUser(this, role.Id));
        }

        /// <summary>
        /// Sets the roles.
        /// </summary>
        /// <param name="roles">The roles.</param>
        public void SetRoles(IEnumerable<Role> roles) {
            if (roles.IsNullOrEmpty()) {
                var tmpRoles = Roles.ToArray();
                foreach (var roleItem in tmpRoles) {
                    RemoveRole(roleItem);
                }

                return;
            }

            if (Roles.IsNullOrEmpty()) {
                foreach (var roleItem in roles) {
                    AddRole(roleItem);
                }

                return;
            }

            var deletedRoles = Roles.Where(ur => !roles.Select(r => r.Id).Contains(ur.Id)).ToArray();
            foreach (var roleItem in deletedRoles) {
                RemoveRole(roleItem);
            }

            var insertedRoles = roles.Where(ur => !Roles.Select(r => r.Id).Contains(ur.Id)).ToArray();
            foreach (var roleItem in insertedRoles) {
                AddRole(roleItem);
            }
        }

        public static User Empty(Guid id) => new User(id, string.Empty, null);
    }
}
