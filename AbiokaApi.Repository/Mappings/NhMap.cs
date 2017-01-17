using AbiokaApi.Domain;
using AbiokaApi.Repository.DatabaseObjects;
using FluentNHibernate.Mapping;

namespace AbiokaApi.Repository.Mappings
{
    internal class UserSecurityMap : SubclassMap<UserSecurity>
    {
        public UserSecurityMap() {
            Map(x => x.AuthProvider);
            Map(x => x.Password);
            Map(x => x.ProviderToken).Length(50);
            Map(x => x.RefreshToken).Length(50);
            Map(x => x.Token).Length(512);
            Map(x => x.Language).Length(10);

            Table("dbo.[UserSecurity]");
        }
    }
    
    internal class UserMap : DeletableClassMap<User>
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

    internal class RoleMap : DeletableClassMap<Role>
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

    internal class UserRoleMap : BaseClassMap<UserRoleDB>
    {
        public UserRoleMap() {
            Id(x => x.Id);
            Map(x => x.UserId);
            //Map(x => x.RoleId);

            References(x => x.Role).Column("RoleId");

            Table("dbo.[UserRole]");
        }
    }

    internal class MenuMap : BaseClassMap<Menu>
    {
        public MenuMap() {
            Id(x => x.Id);
            Map(x => x.Text);
            Map(x => x.Url);
            Map(x => x.Order).Column("[Order]");

            HasMany(x => x.Children)
                    .Inverse()
                    .KeyColumn("ParentId")
                    .OrderBy("[Order] ASC");

            References(x => x.Parent).Column("ParentId");

            Table("dbo.[Menu]");
        }
    }

    internal class LoginAttemptMap : BaseClassMap<LoginAttempt>
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
