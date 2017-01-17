using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Domain;
using System;
using System.Collections.Generic;

namespace AbiokaApi.ApplicationService.DTOs
{
    public class DTOMapper
    {
        private static readonly IDictionary<RuntimeTypeHandle, Func<IEntity, DTO>> mapActions;
        private static readonly IDictionary<RuntimeTypeHandle, Func<DTO, IEntity>> dbMapActions;

        static DTOMapper() {
            mapActions = new Dictionary<RuntimeTypeHandle, Func<IEntity, DTO>>();
            mapActions.Add(typeof(User).TypeHandle, (entity) => ToUserDTO((User)entity));
            mapActions.Add(typeof(UserSecurity).TypeHandle, (entity) => ToUserDTO((User)entity));
            mapActions.Add(typeof(Role).TypeHandle, (entity) => ToRoleDTO((Role)entity));
            mapActions.Add(typeof(LoginAttempt).TypeHandle, (entity) => ToLoginAttemptDTO((LoginAttempt)entity));
            mapActions.Add(typeof(Menu).TypeHandle, (entity) => ToMenuDTO((Menu)entity));

            dbMapActions = new Dictionary<RuntimeTypeHandle, Func<DTO, IEntity>>();
            dbMapActions.Add(typeof(UserDTO).TypeHandle, (entity) => ToUser((UserDTO)entity));
            dbMapActions.Add(typeof(RoleDTO).TypeHandle, (entity) => ToRole((RoleDTO)entity));
            dbMapActions.Add(typeof(MenuDTO).TypeHandle, (entity) => ToMenu((MenuDTO)entity));
        }

        internal static DTO FromDomainObject(IEntity entity) {
            if (entity == null)
                return null;

            var typeHandle = entity.GetType().TypeHandle;
            if (!mapActions.ContainsKey(typeHandle)) {
                throw new NotImplementedException($"{entity.GetType().Name} is not implemented in DTO object mapper.");
            }
            return mapActions[typeHandle](entity);
        }

        internal static IEnumerable<T> FromDomainObject<T>(IEnumerable<IEntity> entities) where T : DTO {
            if (entities == null)
                return null;

            var result = new List<T>();
            foreach (var item in entities) {
                var entity = (T)FromDomainObject(item);
                result.Add(entity);
            }
            return result;
        }

        internal static IEntity ToDomainObject(DTO entity) {
            if (entity == null)
                return null;

            var typeHandle = entity.GetType().TypeHandle;
            if (!dbMapActions.ContainsKey(typeHandle)) {
                throw new NotImplementedException($"{entity.GetType().Name} is not implemented in DTO object mapper.");
            }
            return dbMapActions[typeHandle](entity);
        }

        internal static IEnumerable<T> ToDomainObjects<T>(IEnumerable<DTO> entities) where T : IEntity {
            if (entities == null)
                return null;

            var result = new List<T>();
            foreach (var item in entities) {
                var entity = (T)ToDomainObject(item);
                result.Add(entity);
            }
            return result;
        }

        private static User ToUser(UserDTO userDTO) {
            var result = new User(
              userDTO.Id,
              userDTO.Email,
              ToDomainObjects<Role>(userDTO.Roles)
            );
            return result;
        }

        private static UserDTO ToUserDTO(User user) {
            var result = new UserDTO {
                Id = user.Id,
                Email = user.Email,
                Roles = FromDomainObject<RoleDTO>(user.Roles),
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate
            };
            return result;
        }

        private static Role ToRole(RoleDTO roleDTO) {
            var result = new Role(
                roleDTO.Id,
                roleDTO.Name
            );
            return result;
        }

        private static RoleDTO ToRoleDTO(Role role) {
            var result = new RoleDTO {
                Id = role.Id,
                Name = role.Name,
                CreatedDate = role.CreatedDate,
                UpdatedDate = role.UpdatedDate
            };
            return result;
        }

        private static Menu ToMenu(MenuDTO menuDTO) {
            if (menuDTO == null)
                return null;

            var result = new Menu(
                menuDTO.Id,
                menuDTO.Text,
                menuDTO.Url,
                menuDTO.Order,
                null,
                ToDomainObjects<Menu>(menuDTO.Children)
            );
            return result;
        }

        private static MenuDTO ToMenuDTO(Menu menu) {
            if (menu == null)
                return null;

            var result = new MenuDTO {
                Id = menu.Id,
                Text = menu.Text,
                Url = menu.Url,
                Order = menu.Order,
                Children = FromDomainObject<MenuDTO>(menu.Children),
            };
            return result;
        }

        private static LoginAttemptDTO ToLoginAttemptDTO(LoginAttempt loginAttempt) {
            var result = new LoginAttemptDTO {
                Id = loginAttempt.Id,
                Date = loginAttempt.Date,
                IP = loginAttempt.IP,
                User = ToUserDTO(loginAttempt.User),
                LoginResult = loginAttempt.LoginResult.ToString(),
                CreatedDate = loginAttempt.CreatedDate,
                UpdatedDate = loginAttempt.UpdatedDate
            };
            return result;
        }
    }
}
