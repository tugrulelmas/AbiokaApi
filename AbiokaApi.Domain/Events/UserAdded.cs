using AbiokaApi.Infrastructure.Common.Domain;
using System;

namespace AbiokaApi.Domain.Events
{
    public class UserAdded : IEvent
    {
        public UserAdded(Guid userId) {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
