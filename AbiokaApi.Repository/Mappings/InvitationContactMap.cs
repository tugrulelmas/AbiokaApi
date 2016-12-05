using AbiokaApi.Repository.DatabaseObjects;
using FluentNHibernate.Mapping;

namespace AbiokaApi.Repository.Mappings
{
    internal class InvitationContactMap : ClassMap<InvitationContactDB>
    {
        public InvitationContactMap() {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Email);
            Map(x => x.Phone);
            Map(x => x.Message);
            Map(x => x.IpAddress);
            Table("Invitation.Contact");
        }
    }

    internal class UserSecurityMap : ClassMap<UserSecurityDB>
    {
        public UserSecurityMap() {
            Id(x => x.Id);
            Map(x => x.AuthProvider);
            Map(x => x.Email);
            Map(x => x.IsDeleted);
            Map(x => x.IsAdmin);
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
            Map(x => x.IsAdmin);
            Table("dbo.[User]");
        }
    }
}
