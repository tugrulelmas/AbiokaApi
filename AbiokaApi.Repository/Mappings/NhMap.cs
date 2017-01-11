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
            Map(x => x.ProviderToken).Length(50);
            Map(x => x.RefreshToken).Length(50);
            Map(x => x.Token).Length(512);
            
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

    internal class LoginAttemptMap : ClassMap<LoginAttemptDB>
    {
        public LoginAttemptMap() {
            Id(x => x.Id);
            Map(x => x.Date).Not.Nullable();
            Map(x => x.IP).Not.Nullable();
            Map(x => x.LoginResult).Not.Nullable();
            Map(x => x.Token).Length(512).Nullable();

            References(x => x.User).Column("UserId");

            Table("dbo.[LoginAttempt]");
        }
    }
}
