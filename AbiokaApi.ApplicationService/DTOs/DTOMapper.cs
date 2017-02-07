using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Domain;
using System;
using System.Collections.Generic;

namespace AbiokaApi.ApplicationService.DTOs
{
    public interface IDTOMapper
    {
        T FromDomainObject<T>(IEntity entity) where T : DTO;

        DTO FromDomainObject(IEntity entity);

        IEnumerable<T> FromDomainObject<T>(IEnumerable<IEntity> entities) where T : DTO;

        T ToDomainObject<T>(DTO entity) where T : IEntity;

        IEntity ToDomainObject(DTO entity);

        IEnumerable<T> ToDomainObjects<T>(IEnumerable<DTO> entities) where T : IEntity;
    }

    public class DTOMapper : IDTOMapper
    {
        protected readonly IDictionary<RuntimeTypeHandle, Func<IEntity, DTO>> mapActions;
        protected readonly IDictionary<RuntimeTypeHandle, Func<DTO, IEntity>> dbMapActions;

        public DTOMapper() {
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

        public T FromDomainObject<T>(IEntity entity) where T : DTO => (T)FromDomainObject(entity);

        public DTO FromDomainObject(IEntity entity) {
            if (entity == null)
                return null;

            var typeHandle = entity.GetType().TypeHandle;
            if (mapActions.ContainsKey(typeHandle)) {
                return mapActions[typeHandle](entity);
            }

            var baseTypeHandle = entity.GetType().BaseType.TypeHandle;
            if (mapActions.ContainsKey(baseTypeHandle)) {
                return mapActions[baseTypeHandle](entity);
            }

            throw new NotImplementedException($"{entity.GetType().Name} is not implemented in DTO object mapper.");
        }

        public IEnumerable<T> FromDomainObject<T>(IEnumerable<IEntity> entities) where T : DTO {
            if (entities == null)
                return null;

            var result = new List<T>();
            foreach (var item in entities) {
                var entity = (T)FromDomainObject(item);
                result.Add(entity);
            }
            return result;
        }

        public T ToDomainObject<T>(DTO entity) where T : IEntity => (T)ToDomainObject(entity);

        public IEntity ToDomainObject(DTO entity) {
            if (entity == null)
                return null;

            var typeHandle = entity.GetType().TypeHandle;
            if (!dbMapActions.ContainsKey(typeHandle)) {
                throw new NotImplementedException($"{entity.GetType().Name} is not implemented in DTO object mapper.");
            }
            return dbMapActions[typeHandle](entity);
        }

        public IEnumerable<T> ToDomainObjects<T>(IEnumerable<DTO> entities) where T : IEntity {
            if (entities == null)
                return null;

            var result = new List<T>();
            foreach (var item in entities) {
                var entity = (T)ToDomainObject(item);
                result.Add(entity);
            }
            return result;
        }

        private User ToUser(UserDTO userDTO) {
            var result = new User(
              userDTO.Id,
              userDTO.Email,
              userDTO.Language,
              userDTO.Name,
              userDTO.Surname,
              userDTO.Picture,
              userDTO.Gender,
              ToDomainObjects<Role>(userDTO.Roles)
            );
            return result;
        }

        private UserDTO ToUserDTO(User user) {
            var result = new UserDTO {
                Id = user.Id,
                Email = user.Email,
                Language = user.Language,
                Name = user.Name,
                Surname = user.Surname,
                Picture = user.Picture,
                Gender = user.Gender,
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

        private Menu ToMenu(MenuDTO menuDTO) {
            if (menuDTO == null)
                return null;

            Menu parent = null;
            if (menuDTO.Parent != null) {
                parent = Menu.Create(menuDTO.Parent.Id);
            }

            var result = new Menu(
                menuDTO.Id,
                menuDTO.Text,
                menuDTO.Url,
                menuDTO.Order,
                parent,
                ToDomainObject<Role>(menuDTO.Role),
                ToDomainObjects<Menu>(menuDTO.Children)
            );
            return result;
        }

        private MenuDTO ToMenuDTO(Menu menu) {
            if (menu == null)
                return null;

            MenuDTO parent = null;
            if (menu.Parent != null) {
                parent = new MenuDTO {
                    Id = menu.Parent.Id,
                    Text = menu.Parent.Text
                };
            }

            var result = new MenuDTO {
                Id = menu.Id,
                Text = menu.Text,
                Url = menu.Url,
                Order = menu.Order,
                Parent = parent,
                Role = FromDomainObject<RoleDTO>(menu.Role as Role),
                Children = FromDomainObject<MenuDTO>(menu.Children),
                CreatedDate = menu.CreatedDate,
                UpdatedDate = menu.UpdatedDate
            };
            return result;
        }

        private LoginAttemptDTO ToLoginAttemptDTO(LoginAttempt loginAttempt) {
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
