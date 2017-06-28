using AbiokaApi.Infrastructure.Common.Domain;
using System;

namespace AbiokaApi.Domain.Events
{
    public class UserIsAdded : IEvent
    {
        public UserIsAdded(IIdEntity<Guid> user) {
            User = user;
        }

        public IIdEntity<Guid> User { get; }
    }
}
