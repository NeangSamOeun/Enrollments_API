using ProductWebAPI.DTOs;
using ProductWebAPI.Models;

namespace ProductWebAPI.Repositories
{
    public interface IStudentEnrollmentRepository
    {
        Task<string> CreateStudentEnrollmentAsync(StudentERM erm, CurrentEducation edu, PermanentAddress address, ContactInformation contact, RegisterInformation register);
        Task<bool> ExistsCodeAsync(string code);
        Task<bool> ExistsStudentIdAsync(string studentId);
        Task<List<StudentERM>> GetAllAsync();
        Task<StudentERM?> GetByIdAsync(string studentId);
        Task<PagedResult<StudentListDto>> GetAllWithDtoAsync(int page, int pageSize, string? search);
        Task<StudentEnrollmentDetailDto?> GetStudentDetailAsync(string studentId);
        Task<bool> DeleteStudentAsync(string studentId);
        Task<bool> UpdateStudentPartialAsync(StudentEnrollmentPatchDto dto);
        Task<bool> UpdateStatusAsync(string studentId, string status);
    }
}