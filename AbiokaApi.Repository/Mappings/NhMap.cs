using AbiokaApi.Repository.DatabaseObjects;
using FluentNHibernate.Mapping;

namespace AbiokaApi.Repository.Mappings
{
    internal class UserSecurityMap : ClassMap<UserSecurityDB>
    {
        public UserSecurityMap() {
            Id(x => x.Id);
            Map(x => x.AuthProvider);
            Map(x => x.Email);
            Map(x => x.IsDeleted);
            Map(x => x.Password);
            Map(x => x.ProviderToken);
            Map(x => x.Token);
            
            Table("dbo.[User]");
        }
    }
    
    internal class UserMap : DeletableClassMap<UserDB>
    {
        public UserMap() {
            Id(x => x.Id);
            Map(x => x.Email);
            /*
            HasManyToMany(x => x.Roles)
                .Table("dbo.UserRole")
                .ParentKeyColumn("UserId")
                .ChildKeyColumn("RoleId")
                .LazyLoad()
                .Cascade.All();
            */
            Table("dbo.[User]");
        }
    }

    internal class RoleMap : DeletableClassMap<RoleDB>
    {
        public RoleMap() {
            Id(x => x.Id);
            Map(x => x.Name);
            /*
            HasMany(x => x.UserRoles)
                .Table("dbo.UserRole")
                .KeyColumn("RoleId");
                */
            Table("dbo.[Role]");
        }
    }

    internal class UserRoleMap : ClassMap<UserRoleDB>
    {
        public UserRoleMap() {
            Id(x => x.Id);
            Map(x => x.UserId);
            //Map(x => x.RoleId);

            References(x => x.Role).Column("RoleId");

            Table("dbo.[UserRole]");
        }
    }
}
