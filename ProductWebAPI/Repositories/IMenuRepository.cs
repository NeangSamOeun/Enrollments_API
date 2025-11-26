using ProductWebAPI.Models;

namespace ProductWebAPI.Repositories
{
    public interface IMenuRepository
    {
        Task<Menu> AddMenuAsync(Menu menu);
        IQueryable<Menu> GetAllMenu();
    }
}