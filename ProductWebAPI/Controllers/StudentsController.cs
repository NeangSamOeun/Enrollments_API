using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.DTOs;
using ProductWebAPI.Models;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDbContext _context;
        public StudentsController(StudentDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            return await _context.Students.Select(s => new StudentDTO
            {
                Id = s.Id,
                FullName = s.FullName,
                Gender = s.Gender,
                DateOfBirth = s.DateOfBirth,
                Email = s.Email
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            return new StudentDTO
            {
                Id = student.Id,
                FullName = student.FullName,
                Gender = student.Gender,
                DateOfBirth = student.DateOfBirth,
                Email = student.Email
            };
        }

        [HttpPost]
        public async Task<ActionResult<StudentDTO>> CreateStudent(StudentDTO dto)
        {
            var student = new Student
            {
                FullName = dto.FullName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            dto.Id = student.Id;
            return CreatedAtAction(nameof(GetStudent), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(string id, StudentDTO dto)
        {
            if (id != dto.Id) return BadRequest();

            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            student.FullName = dto.FullName;
            student.Gender = dto.Gender;
            student.DateOfBirth = dto.DateOfBirth;
            student.Email = dto.Email;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
