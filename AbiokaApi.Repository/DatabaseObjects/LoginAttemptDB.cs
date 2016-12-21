using System;

namespace AbiokaApi.Repository.DatabaseObjects
{
    public class LoginAttemptDB : DBEntity
    {
        public virtual Guid Id { get; set; }

        public virtual UserDB User { get; set; }

        public virtual string Token { get; set; }

        public virtual string IP { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual string LoginResult { get; set; }
    }
}
