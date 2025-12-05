using ProductWebAPI.DTOs;

namespace ProductWebAPI.Services
{
    public interface IMajorService
    {
        Task<MajorDto> Create(MajorCreateUpdateDto dto);
        Task<bool> Delete(int id);
        Task<List<MajorDto>> GetAll();
        Task<MajorDto?> GetById(int id);
        Task<MajorDto?> Update(int id, MajorCreateUpdateDto dto);
    }
}