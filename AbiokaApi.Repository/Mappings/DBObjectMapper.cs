using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Repository.DatabaseObjects;
using System;
using System.Collections.Generic;

namespace AbiokaApi.Repository.Mappings
{
    internal class DBObjectMapper
    {
        private static readonly IDictionary<RuntimeTypeHandle, Func<IEntity, DBEntity>> mapActions;

        static DBObjectMapper() {
            mapActions = new Dictionary<RuntimeTypeHandle, Func<IEntity, DBEntity>>();
            mapActions.Add(typeof(InvitationContact).TypeHandle, (entity) => ToInvitationContactDB((InvitationContact)entity));
        }

        internal static DBEntity FromDomainObject(IEntity entity) {
            var typeHandle = entity.GetType().TypeHandle;
            if (!mapActions.ContainsKey(typeHandle))
            {
                throw new NotImplementedException($"{entity.GetType().Name} is not implemented in MongoDB object mapper.");
            }
            return mapActions[typeHandle](entity);
        }

        private static InvitationContactDB ToInvitationContactDB(InvitationContact invitationContact) {
            var result = new InvitationContactDB
            {
                Id = invitationContact.Id,
                Name = invitationContact.Name,
                Email = invitationContact.Email,
                Phone = invitationContact.Phone,
                Message = invitationContact.Message,
                IpAddress = invitationContact.IpAddress
            };
            return result;
        }
    }
}
