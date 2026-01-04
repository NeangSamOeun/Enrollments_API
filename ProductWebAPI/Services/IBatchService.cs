using ProductWebAPI.DTOs;

namespace ProductWebAPI.Services
{
    public interface IBatchService
    {
        Task<BatchCreateDto> CreateAsync(BatchCreateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<BatchCreateDto>> GetAllAsync();
        Task<BatchCreateDto?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, BatchCreateDto dto);
    }
}