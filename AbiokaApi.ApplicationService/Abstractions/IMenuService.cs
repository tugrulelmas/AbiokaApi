using AbiokaApi.ApplicationService.DTOs;
using System.Collections.Generic;

namespace AbiokaApi.ApplicationService.Abstractions
{
    public interface IMenuService : ICrudService<MenuDTO>
    {
        IEnumerable<MenuDTO> Filter(string text);
    }
}