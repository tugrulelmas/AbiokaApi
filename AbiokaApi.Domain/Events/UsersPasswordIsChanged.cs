using AbiokaApi.Infrastructure.Common.Domain;
using System;

namespace AbiokaApi.Domain.Events
{
    public class UsersPasswordIsChanged : IEvent
    {
        public UsersPasswordIsChanged(Guid userId, string email) {
            UserId = userId;
            Email = email;
        }

        public Guid UserId { get; }

        public string Email { get; set; }
    }
}
