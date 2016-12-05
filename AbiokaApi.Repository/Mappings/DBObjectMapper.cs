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
        private static readonly IDictionary<RuntimeTypeHandle, Func<DBEntity, IEntity>> dbMapActions;

        static DBObjectMapper() {
            mapActions = new Dictionary<RuntimeTypeHandle, Func<IEntity, DBEntity>>();
            mapActions.Add(typeof(InvitationContact).TypeHandle, (entity) => ToInvitationContactDB((InvitationContact)entity));
            mapActions.Add(typeof(UserSecurity).TypeHandle, (entity) => ToUserSecurityDB((UserSecurity)entity));
            mapActions.Add(typeof(User).TypeHandle, (entity) => ToUserDB((User)entity));

            dbMapActions = new Dictionary<RuntimeTypeHandle, Func<DBEntity, IEntity>>();
            dbMapActions.Add(typeof(UserSecurityDB).TypeHandle, (entity) => ToUserSecurity((UserSecurityDB)entity));
            dbMapActions.Add(typeof(UserDB).TypeHandle, (entity) => ToUser((UserDB)entity));
            dbMapActions.Add(typeof(InvitationContact).TypeHandle, (entity) => ToInvitationContact((InvitationContactDB)entity));
        }

        internal static DBEntity FromDomainObject(IEntity entity) {
            var typeHandle = entity.GetType().TypeHandle;
            if (!mapActions.ContainsKey(typeHandle)) {
                throw new NotImplementedException($"{entity.GetType().Name} is not implemented in MongoDB object mapper.");
            }
            return mapActions[typeHandle](entity);
        }

        internal static IEntity ToDomainObject(DBEntity entity) {
            var typeHandle = entity.GetType().TypeHandle;
            if (!dbMapActions.ContainsKey(typeHandle)) {
                throw new NotImplementedException($"{entity.GetType().Name} is not implemented in MongoDB object mapper.");
            }
            return dbMapActions[typeHandle](entity);
        }

        internal static IEnumerable<T> ToDomainObjects<T>(IEnumerable<DBEntity> entities) where T : IEntity {
            var result = new List<T>();
            foreach (var item in entities) {
                var entity = (T)ToDomainObject(item);
                result.Add(entity);
            }
            return result;
        }

        private static InvitationContact ToInvitationContact(InvitationContactDB invitationContactDB) {
            var result = new InvitationContact {
                Id = invitationContactDB.Id,
                Name = invitationContactDB.Name,
                Email = invitationContactDB.Email,
                Phone = invitationContactDB.Phone,
                Message = invitationContactDB.Message,
                IpAddress = invitationContactDB.IpAddress
            };
            return result;
        }

        private static InvitationContactDB ToInvitationContactDB(InvitationContact invitationContact) {
            var result = new InvitationContactDB {
                Id = invitationContact.Id,
                Name = invitationContact.Name,
                Email = invitationContact.Email,
                Phone = invitationContact.Phone,
                Message = invitationContact.Message,
                IpAddress = invitationContact.IpAddress
            };
            return result;
        }

        private static UserSecurity ToUserSecurity(UserSecurityDB userDB) {
            var result = new UserSecurity {
                Id = userDB.Id,
                Email = userDB.Email,
                AuthProvider = userDB.AuthProvider,
                ProviderToken = userDB.ProviderToken,
                Token = userDB.Token,
                Password = userDB.Password,
                IsAdmin = userDB.IsAdmin,
                IsActive = userDB.IsActive
            };
            return result;
        }

        private static UserSecurityDB ToUserSecurityDB(UserSecurity user) {
            var result = new UserSecurityDB {
                Id = user.Id,
                AuthProvider = user.AuthProvider,
                Email = user.Email,
                IsActive = user.IsActive,
                IsAdmin = user.IsAdmin,
                Password = user.Password,
                ProviderToken = user.ProviderToken,
                Token = user.Token
            };
            return result;
        }

        private static User ToUser(UserDB userDB) {
            var result = new User {
                Id = userDB.Id,
                Email = userDB.Email,
                IsAdmin = userDB.IsAdmin
            };
            return result;
        }

        private static UserDB ToUserDB(User user) {
            var result = new UserDB {
                Id = user.Id,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };
            return result;
        }
    }
}
