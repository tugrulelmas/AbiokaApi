using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Repository.DatabaseObjects;

namespace AbiokaApi.Repository.Repositories
{
    internal class InvitationContactRepository : Repository<InvitationContact, InvitationContactDB>, IInvitationContactRepository
    {
    }
}
