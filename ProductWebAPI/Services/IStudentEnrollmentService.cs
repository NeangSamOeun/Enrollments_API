using ProductWebAPI.DTOs;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services
{
    public interface IStudentEnrollmentService
    {
        Task<List<StudentERMDTO>> GetAllAsync();
        Task<string> CreateEnrollmentAsync(StudentEnrollmentDto dto);
        Task<PagedResult<StudentListDto>> GetListAsync(int page, int pageSize, string? search);
        Task<StudentEnrollmentDetailDto?> GetDetailAsync(string studentId);


    }
}