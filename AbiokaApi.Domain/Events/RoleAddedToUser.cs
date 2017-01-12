using AbiokaApi.Infrastructure.Common.Domain;
using System;

namespace AbiokaApi.Domain.Events
{
    public class RoleAddedToUser : IEvent
    {
        public RoleAddedToUser(IIdEntity<Guid> user, Guid roleId) {
            User = user;
            RoleId = roleId;
        }

        public IIdEntity<Guid> User { get; }

        public Guid RoleId { get; }
    }
}
