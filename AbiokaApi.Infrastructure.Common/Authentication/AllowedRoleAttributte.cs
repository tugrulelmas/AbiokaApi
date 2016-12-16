using System;

namespace AbiokaApi.Infrastructure.Common.Authentication
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class AllowedRole : Attribute
    {
        public AllowedRole(params string[] roles) {
            Roles = roles;
        }

        public string[] Roles { get; set; }
    }
}
