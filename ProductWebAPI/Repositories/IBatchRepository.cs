using ProductWebAPI.Models;

namespace ProductWebAPI.Repositories
{
    public interface IBatchRepository
    {
        Task<Batch> AddAsync(Batch batch);
        Task DeleteAsync(Batch batch);
        Task<IEnumerable<Batch>> GetAllAsync();
        Task<Batch?> GetByIdAsync(int id);
        Task<bool> SaveChangesAsync();
        Task UpdateAsync(Batch batch);
    }
}