using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Repository.DatabaseObjects;

namespace AbiokaApi.Repository.Repositories
{
    public class InvitationContactRepository : Repository<InvitationContact, InvitationContactDB>, IInvitationContactRepository
    {
    }
}
