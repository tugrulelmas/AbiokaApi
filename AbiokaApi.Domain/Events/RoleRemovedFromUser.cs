using AbiokaApi.Infrastructure.Common.Domain;
using System;

namespace AbiokaApi.Domain.Events
{
    public class RoleRemovedFromUser : IEvent
    {
        public RoleRemovedFromUser(Guid userId, Guid roleId) {
            UserId = userId;
            RoleId = roleId;
        }

        public Guid UserId { get; }

        public Guid RoleId { get; }
    }
}
