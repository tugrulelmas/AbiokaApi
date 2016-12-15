using System;

namespace AbiokaApi.Infrastructure.Common.Authentication
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class AllowedRoleAttributte : Attribute
    {
        public AllowedRoleAttributte(params string[] roles) {
            Roles = roles;
        }

        public string[] Roles { get; set; }
    }
}
