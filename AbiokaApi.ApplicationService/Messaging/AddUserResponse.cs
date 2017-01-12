using AbiokaApi.Domain;
using System;
using System.Collections.Generic;

namespace AbiokaApi.ApplicationService.Messaging
{
    public class AddUserResponse : ServiceResponseBase
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public IEnumerable<Role> Roles { get; set; }
    }
}
