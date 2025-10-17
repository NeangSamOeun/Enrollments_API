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
    public class CoursesController : ControllerBase
    {
        private readonly StudentDbContext _context;
        public CoursesController(StudentDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            return await _context.Courses.Select(c => new CourseDTO
            {
                Id = c.Id,
                CourseCode = c.CourseCode,
                CourseName = c.CourseName,
                Credits = c.Credits,
                Fee = c.Fee
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(string id)
        {
            var c = await _context.Courses.FindAsync(id);
            if (c == null) return NotFound();

            return new CourseDTO
            {
                Id = c.Id,
                CourseCode = c.CourseCode,
                CourseName = c.CourseName,
                Credits = c.Credits,
                Fee = c.Fee
            };
        }

        [HttpPost]
        public async Task<ActionResult<CourseDTO>> CreateCourse(CourseDTO dto)
        {
            var course = new Course
            {
                CourseCode = dto.CourseCode,
                CourseName = dto.CourseName,
                Credits = dto.Credits,
                Fee = dto.Fee
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            dto.Id = course.Id;
            return CreatedAtAction(nameof(GetCourse), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(string id, CourseDTO dto)
        {
            if (id != dto.Id) return BadRequest();

            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            course.CourseCode = dto.CourseCode;
            course.CourseName = dto.CourseName;
            course.Credits = dto.Credits;
            course.Fee = dto.Fee;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(string id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
