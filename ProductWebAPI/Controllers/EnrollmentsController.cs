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
    public class EnrollmentsController : ControllerBase
    {
        private readonly StudentDbContext _context;
        public EnrollmentsController(StudentDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentDTO>>> GetEnrollments()
        {
            return await _context.Enrollments.Select(e => new EnrollmentDTO
            {
                Id = e.Id,
                StudentId = e.StudentId,
                CourseId = e.CourseId,
                EnrollDate = e.EnrollDate
            }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<EnrollmentDTO>> CreateEnrollment(EnrollmentDTO dto)
        {
            var student = await _context.Students.FindAsync(dto.StudentId);
            var course = await _context.Courses.FindAsync(dto.CourseId);
            if (student == null || course == null) return BadRequest("Invalid Student or Course ID");

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                EnrollDate = dto.EnrollDate
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            dto.Id = enrollment.Id;
            return CreatedAtAction(nameof(GetEnrollments), new { id = dto.Id }, dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(string id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) return NotFound();

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
