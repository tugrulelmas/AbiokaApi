using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Helper;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class MenuService : CrudService<Menu, MenuDTO>, IMenuService
    {
        private readonly ICurrentContext currentContext;

        public MenuService(IRepository<Menu> repository, ICurrentContext currentContext, IDTOMapper dtoMapper)
            : base(repository, dtoMapper) {
            this.currentContext = currentContext;
        }

        public override IEnumerable<MenuDTO> GetAll() {
            var menus = repository.Query().Where(m => m.Parent == null).ToList();
            IEnumerable<Menu> result = null;
            if (!currentContext.Current.Principal.IsInRole("Admin")) {
                result = menus.Where(m => currentContext.Current.Principal.Roles.Contains(m.Role.Name)).OrderBy(m => m.Order).ToList();
            } else {
                result = menus.OrderBy(m => m.Order).ToList();
            }

            return dtoMapper.FromDomainObject<MenuDTO>(result);
        }

        public IEnumerable<MenuDTO> Filter(string text) {
            IEnumerable<Menu> result = null;
            if (string.IsNullOrWhiteSpace(text)) {
                result = repository.Query().Take(5).ToList();
            } else {
                result = repository.Query().Where(m => m.Text.Contains(text)).ToList();
            }

            return dtoMapper.FromDomainObject<MenuDTO>(result);
        }
    }
}
