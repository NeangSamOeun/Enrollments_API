using ProductWebAPI.Models;

namespace ProductWebAPI.Services
{
    public interface IMenuService
    {
        Task<Menu> CreateMenuAsync(Menu menu);
        IQueryable<Menu> GetMenus();
    }
}