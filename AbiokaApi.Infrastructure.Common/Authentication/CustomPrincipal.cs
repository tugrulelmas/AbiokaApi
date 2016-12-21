using System;
using System.Linq;
using System.Security.Principal;

namespace AbiokaApi.Infrastructure.Common.Authentication
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public CustomPrincipal(string userName) {
            Identity = new GenericIdentity(userName);
        }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public IIdentity Identity { get; }

        public DateTime TokenExpirationDate { get; set; }

        public bool IsInRole(string role) => Roles.Where(r => r == role).Any();

        public string[] Roles { get; set; }

        public Guid Id { get; set; }
    }
}
