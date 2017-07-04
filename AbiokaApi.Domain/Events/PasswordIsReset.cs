using AbiokaApi.Infrastructure.Common.Domain;
using System;

namespace AbiokaApi.Domain.Events
{
    public class PasswordIsReset : IEvent
    {
        public PasswordIsReset(IIdEntity<Guid> user) {
            User = user;
        }

        public IIdEntity<Guid> User { get; }
    }
}
