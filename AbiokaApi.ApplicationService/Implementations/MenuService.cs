using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class MenuService : CrudService<Menu, MenuDTO>, IMenuService
    {
        public MenuService(IMenuRepository repository)
            : base(repository) {
        }
    }
}
