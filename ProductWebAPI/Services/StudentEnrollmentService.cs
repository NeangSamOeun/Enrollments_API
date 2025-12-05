using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.DTOs;
using ProductWebAPI.Extensions;
using ProductWebAPI.Models;
using ProductWebAPI.Repositories;

namespace ProductWebAPI.Services
{
    public class StudentEnrollmentService : IStudentEnrollmentService
    {
        private readonly IStudentEnrollmentRepository _repo;
        public StudentEnrollmentService(IStudentEnrollmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<PagedResult<StudentListDto>> GetListAsync(int page, int pageSize, string? search)
        {
            var getResults = await _repo.GetAllWithDtoAsync(page, pageSize, search);
            return getResults;
        }

        // get detail 
         public async Task<StudentEnrollmentDetailDto?> GetDetailAsync(string studentId)
         {
            var getDetails = await _repo.GetStudentDetailAsync(studentId);
            return getDetails;
         }

        public async Task<List<StudentERMDTO>> GetAllAsync()
        {
            var results = await _repo.GetAllAsync();
            return results.Select(r => r.ToDto()).ToList();
        }

        public async Task<string> CreateEnrollmentAsync(StudentEnrollmentDto dto)
        {
            var existingCode = await _repo.ExistsCodeAsync(dto.Code);
            if (existingCode) return "DUPLICATE_CODE";

            string studentId;
            do { studentId = Guid.NewGuid().ToString(); }
            while (await _repo.ExistsStudentIdAsync(studentId));

            var erm = dto.ToERM(studentId);
            var edu = dto.ToEducation(studentId);
            var address = dto.ToAddress(studentId);
            var contact = dto.ToContact(studentId);
            var register = dto.ToRegister(studentId);
            
            var EnrolledStudent = await _repo.CreateStudentEnrollmentAsync(
                erm, edu, address, contact, register
            );

            return EnrolledStudent;
        }                            
                 
    }                                              
}                       
                                            