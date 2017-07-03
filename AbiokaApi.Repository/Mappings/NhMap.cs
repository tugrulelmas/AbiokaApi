using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Repository.DatabaseObjects;
using FluentNHibernate.Mapping;

namespace AbiokaApi.Repository.Mappings
{
    public class UserSecurityMap : SubclassMap<UserSecurity>
    {
        public UserSecurityMap() {
            Map(x => x.AuthProvider).Length(20);
            Map(x => x.Password);
            Map(x => x.ProviderToken).Length(512);
            Map(x => x.ProviderRefreshToken).Length(512);
            Map(x => x.RefreshToken).Length(50);
            Map(x => x.Token).Length(512);
            Map(x => x.IsEmailVerified).Not.Nullable();

            Table("dbo.[UserSecurity]");
        }
    }
    
    public class UserMap : DeletableClassMap<User>
    {
        public UserMap() {
            Id(x => x.Id);
            Map(x => x.Email);
            Map(x => x.Language).Length(10);
            Map(x => x.Gender).Length(10);
            Map(x => x.Name).Length(100);
            Map(x => x.Surname).Length(100);
            Map(x => x.Picture).Length(250);
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

    public class RoleMap : DeletableClassMap<Role>
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

    public class UserRoleMap : BaseClassMap<UserRoleDB>
    {
        public UserRoleMap() {
            Id(x => x.Id);
            Map(x => x.UserId);
            //Map(x => x.RoleId);

            References(x => x.Role).Column("RoleId");

            Table("dbo.[UserRole]");
        }
    }

    public class MenuMap : BaseClassMap<Menu>
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
            References(x => x.Role).Column("RoleId").Fetch.Join();

            Table("dbo.[Menu]");
        }
    }

    public class LoginAttemptMap : BaseClassMap<LoginAttempt>
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

    public class ExceptionLogMap : BaseClassMap<ExceptionLog>
    {
        public ExceptionLogMap() {
            Id(x => x.Id);
            Map(x => x.ErrorCode).Length(1000);
            Map(x => x.Message).Length(8000);
            Map(x => x.Request).Length(8000);
            Map(x => x.Source).Length(100);
            Map(x => x.TypeName).Length(100);
            Map(x => x.UserId);
            Map(x => x.IP);

            Table("dbo.[ExceptionLog]");
        }
    }
}
