using ProductWebAPI.Models;

namespace ProductWebAPI.Repositories
{
    public interface IMajorRepository
    {
        Task<Majors> CreateAsync(Majors major);
        Task<bool> DeleteAsync(int id);
        Task<List<Majors>> GetAllAsync();
        Task<Majors?> GetByIdAsync(int id);
        Task<Majors> UpdateAsync(Majors major);
    }
}