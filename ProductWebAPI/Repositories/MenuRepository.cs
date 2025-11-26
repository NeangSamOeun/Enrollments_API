using ProductWebAPI.Data;
using ProductWebAPI.Models;

namespace ProductWebAPI.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly StudentDbContext _dbContext;
        public MenuRepository(StudentDbContext context)
        {
            _dbContext = context;
        }

        public IQueryable<Menu> GetAllMenu()
        {
            var menus = _dbContext.Menus.AsQueryable();
            return menus;
        }

        public async Task<Menu> AddMenuAsync(Menu menu)
        {
            var createdMenu = await _dbContext.Menus.AddAsync(menu);
            await _dbContext.SaveChangesAsync();
            return createdMenu.Entity;
        }
    }
}
