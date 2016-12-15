﻿using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Repository.DatabaseObjects;
using System;
using System.Collections.Generic;
using AbiokaApi.Infrastructure.Common.Helper;
using System.Linq;

namespace AbiokaApi.Repository.Mappings
{
    public class DBObjectMapper
    {
        private static readonly IDictionary<RuntimeTypeHandle, Func<IEntity, DBEntity>> mapActions;
        private static readonly IDictionary<RuntimeTypeHandle, Func<DBEntity, IEntity>> dbMapActions;

        static DBObjectMapper() {
            mapActions = new Dictionary<RuntimeTypeHandle, Func<IEntity, DBEntity>>();
            mapActions.Add(typeof(UserSecurity).TypeHandle, (entity) => ToUserSecurityDB((UserSecurity)entity));
            mapActions.Add(typeof(User).TypeHandle, (entity) => ToUserDB((User)entity));
            mapActions.Add(typeof(Role).TypeHandle, (entity) => ToRoleDB((Role)entity));

            dbMapActions = new Dictionary<RuntimeTypeHandle, Func<DBEntity, IEntity>>();
            dbMapActions.Add(typeof(UserSecurityDB).TypeHandle, (entity) => ToUserSecurity((UserSecurityDB)entity));
            dbMapActions.Add(typeof(UserDB).TypeHandle, (entity) => ToUser((UserDB)entity));
            dbMapActions.Add(typeof(RoleDB).TypeHandle, (entity) => ToRole((RoleDB)entity));
        }

        internal static DBEntity FromDomainObject(IEntity entity) {
            var typeHandle = entity.GetType().TypeHandle;
            if (!mapActions.ContainsKey(typeHandle)) {
                throw new NotImplementedException($"{entity.GetType().Name} is not implemented in MongoDB object mapper.");
            }
            return mapActions[typeHandle](entity);
        }

        internal static IEnumerable<T> FromDomainObject<T>(IEnumerable<IEntity> entities) where T : DBEntity {
            var result = new List<T>();
            foreach (var item in entities) {
                var entity = (T)FromDomainObject(item);
                result.Add(entity);
            }
            return result;
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

        private static UserSecurity ToUserSecurity(UserSecurityDB userDB) {
            var result = new UserSecurity {
                Id = userDB.Id,
                Email = userDB.Email,
                AuthProvider = userDB.AuthProvider.EnumParse<AuthProvider>(),
                ProviderToken = userDB.ProviderToken,
                Token = userDB.Token,
                Password = userDB.Password,
                IsDeleted = userDB.IsDeleted
            };
            return result;
        }

        private static UserSecurityDB ToUserSecurityDB(UserSecurity user) {
            var result = new UserSecurityDB {
                Id = user.Id,
                AuthProvider = user.AuthProvider.ToString(),
                Email = user.Email,
                IsDeleted = user.IsDeleted,
                Password = user.Password,
                ProviderToken = user.ProviderToken,
                Token = user.Token
            };
            return result;
        }

        private static User ToUser(UserDB userDB) {
            var result = new User {
                Id = userDB.Id,
                Email = userDB.Email
            };
            return result;
        }

        private static UserDB ToUserDB(User user) {
            var result = new UserDB {
                Id = user.Id,
                Email = user.Email
            };
            return result;
        }

        private static Role ToRole(RoleDB roleDB) {
            var result = new Role {
                Id = roleDB.Id,
                Name = roleDB.Name
            };
            return result;
        }

        private static RoleDB ToRoleDB(Role role) {
            var result = new RoleDB {
                Id = role.Id,
                Name = role.Name,
            };
            return result;
        }
    }
}
