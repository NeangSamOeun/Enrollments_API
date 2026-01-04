using ProductWebAPI.DTOs;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services
{
    public interface IStudentEnrollmentService
    {
        Task<List<StudentERMDTO>> GetAllAsync();
        Task<StudentERM?> GetByIdAsync(string studentId);

        Task<string> CreateEnrollmentAsync(StudentEnrollmentDto dto);
        Task<PagedResult<StudentListDto>> GetListAsync(int page, int pageSize, string? search);
        Task<StudentEnrollmentDetailDto?> GetDetailAsync(string studentId);
        Task<bool> DeleteStudentAsync(string studentId);
        Task<bool> UpdateStudentPartialAsync(StudentEnrollmentPatchDto dto);
        Task<bool> UpdateStatusAsync(UpdateStatusDto dto);



    }
}