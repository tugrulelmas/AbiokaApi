using AbiokaApi.Infrastructure.Common.Domain;
using System;

namespace AbiokaApi.Domain.Events
{
    public class UserDeleted : IEvent
    {
        public UserDeleted(Guid userId) {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
