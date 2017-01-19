using AbiokaApi.Domain.Events;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaApi.Domain
{
    public class User : DeletableEntity
    {
        protected readonly List<Role> roles;

        public User() {
            roles = new List<Role>();
        }

        public User(Guid id, string email, string language, string name, string surname, string picture, Gender gender, IEnumerable<Role> roles)
            : this() {
            Id = id;
            Email = email;
            Language = language;
            Name = name;
            Surname = surname;
            Gender = gender;
            Picture = picture;

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
        public virtual string Email { get; protected set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public virtual string Language { get; protected set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>
        /// The surname.
        /// </value>
        public virtual string Surname { get; protected set; }

        /// <summary>
        /// Gets or sets the picture.
        /// </summary>
        /// <value>
        /// The picture.
        /// </value>
        public virtual string Picture { get; protected set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public virtual Gender Gender { get; protected set; }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public virtual IEnumerable<Role> Roles => roles;

        /// <summary>
        /// Adds the role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <exception cref="DenialException"></exception>
        public virtual void AddRole(Role role) {
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
        public virtual void RemoveRole(Role role) {
            var tmpRole = roles.FirstOrDefault(r => r.Id == role.Id);
            if (tmpRole == null)
                throw new DenialException($"The user with email {Email} has not a role with id {role.Id}");

            roles.Remove(role);
            AddEvent(new RoleRemovedFromUser(this, role.Id));
        }

        public virtual void Update(User user) {
            Name = user.Name;
            Surname = user.Surname;
            Gender = user.Gender;

            SetRoles(user.Roles);
        }

        /// <summary>
        /// Sets the roles.
        /// </summary>
        /// <param name="roles">The roles.</param>
        public virtual void SetRoles(IEnumerable<Role> roles) {
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

        public virtual void ChangeLanguage(string language) {
            if (string.IsNullOrWhiteSpace(language))
                throw new DenialException("LanguageCannotBeEmpty");

            Language = language;
        }

        public static User Empty(Guid id) => new User(id, null, null, null, null, null, Gender.Male, null);

        public static User Empty(Guid id, IEnumerable<Role> roles) => new User(id, null, null, null, null, null, Gender.Male, roles);
    }
}
