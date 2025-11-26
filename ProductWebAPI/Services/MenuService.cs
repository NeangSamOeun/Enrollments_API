using ProductWebAPI.Models;
using ProductWebAPI.Repositories;

namespace ProductWebAPI.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repo;
        public MenuService(IMenuRepository repo)
        {
            _repo = repo;
        }

        public IQueryable<Menu> GetMenus()
        {
            var menus = _repo.GetAllMenu();
            return menus;
        }

        public async Task<Menu> CreateMenuAsync(Menu menu)
        {
            var createdMenu = await _repo.AddMenuAsync(menu);
            return createdMenu;
        }
    }
}
