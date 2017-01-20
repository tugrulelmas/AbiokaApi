using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaApi.Domain
{
    public class UserSecurity : User
    {
        public UserSecurity()
            : base() {

        }

        public UserSecurity(Guid id, string email, AuthProvider authProvider, string providerToken, string providerRefreshToken, string refreshToken, string token, string password, string language, string name, string surname, string picture, Gender gender, bool isDeleted, IEnumerable<Role> roles)
            : base(id, email, language, name, surname, picture, gender, roles) {
            AuthProvider = authProvider;
            ProviderToken = providerToken;
            ProviderRefreshToken = providerRefreshToken;
            RefreshToken = refreshToken;
            Token = token;
            IsDeleted = isDeleted;

            if (Id.IsNullOrEmpty()) {
                ComputeHashPassword(password);
            } else {
                Password = password;
            }
        }

        /// <summary>
        /// Gets or sets the authentication provider.
        /// </summary>
        /// <value>
        /// The authentication provider.
        /// </value>
        public virtual AuthProvider AuthProvider { get; protected set; }

        /// <summary>
        /// Gets or sets the provider token.
        /// </summary>
        /// <value>
        /// The provider token.
        /// </value>
        public virtual string ProviderToken { get; protected set; }

        /// <summary>
        /// Gets or sets the provider refresh token.
        /// </summary>
        /// <value>
        /// The provider refresh token.
        /// </value>
        public virtual string ProviderRefreshToken { get; protected set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        /// <value>
        /// The refresh token.
        /// </value>
        public virtual string RefreshToken { get; protected set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public virtual string Token { get; protected set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public virtual string Password { get; protected set; }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="abiokaToken">The abioka token.</param>
        public virtual void CreateToken(IAbiokaToken abiokaToken, string newProviderToken) {
            var userInfo = new UserClaim {
                Email = Email,
                Id = Id,
                Provider = AuthProvider,
                ProviderToken = newProviderToken,
                Roles = Roles?.Select(r => r.Name).ToArray(),
                RefreshToken = RefreshToken,
                Language = Language
            };

            var token = abiokaToken.Encode(userInfo);
            Token = token;
            ProviderToken = newProviderToken;
        }

        public virtual void ChangePassword(string oldPassword, string newPassword) {
            if(AuthProvider != AuthProvider.Local)
                throw new DenialException("PasswordCannotBeChanged");

            if (string.IsNullOrWhiteSpace(newPassword))
                throw new DenialException("PasswordCannotBeEmpty");

            if(!ArePasswordEqual(Email, oldPassword))
                throw new DenialException("WrongPassword");

            if(ArePasswordEqual(Email, newPassword))
                throw new DenialException("NewPasswordCannotBeSameAsTheOldPassword");

            ComputeHashPassword(newPassword);
            RefreshToken = Guid.NewGuid().ToString();
        }

        public virtual void UpdateProviderRefreshToken(string refreshToken) {
            ProviderRefreshToken = refreshToken;
        }

        /// <summary>
        /// Are the passwords equal.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public virtual bool ArePasswordEqual(string email, string password) => Password == ComputeHashPassword(email, password);

        private void ComputeHashPassword(string password) {
            if (string.IsNullOrEmpty(password))
                return;

            Password = ComputeHashPassword(Email, password);
        }

        private string ComputeHashPassword(string email, string password) => Util.GetHashText(string.Concat(email.ToLowerInvariant(), "#", password));

        public static UserSecurity CreateBasic(Guid id, string email, string password, bool isDeleted = false) => new UserSecurity(id, email, AuthProvider.Local, string.Empty, null, string.Empty, string.Empty, password, string.Empty, null, null, null, Gender.Male, isDeleted, null);
    }
}