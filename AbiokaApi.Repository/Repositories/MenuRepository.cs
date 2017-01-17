using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaApi.Repository.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public override IEnumerable<Menu> GetAll() {
            var result = Query.Where(m => m.Parent == null).OrderBy(m => m.Order).ToList();
            return result;
        }
    }
}
