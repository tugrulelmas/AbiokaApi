using System;

namespace AbiokaApi.Repository.DatabaseObjects
{
    internal class UserSecurityDB : DBEntity
    {
        public virtual Guid Id { get; set; }

        public virtual string Email { get; set; }

        public virtual string AuthProvider { get; set; }

        public virtual string ProviderToken { get; set; }

        public virtual string Token { get; set; }

        public virtual string Password { get; set; }

        public virtual bool IsAdmin { get; set; }

        public virtual bool IsActive { get; set; }
    }

    internal class UserDB : DBEntity
    {
        public virtual Guid Id { get; set; }

        public virtual string Email { get; set; }

        public virtual bool IsAdmin { get; set; }
    }
}
