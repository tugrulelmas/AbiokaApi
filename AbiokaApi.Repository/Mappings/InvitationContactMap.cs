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
            Table("dbo.InvitationContact");
        }
    }
}
