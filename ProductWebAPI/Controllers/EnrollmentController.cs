using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.DTOs;
using ProductWebAPI.Services;

namespace ProductWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IStudentEnrollmentService _service;
        public EnrollmentController(IStudentEnrollmentService service)
        {
            _service = service;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromQuery] string? search)
        {
            var results = await _service.GetListAsync(page, pageSize, search);
            return Ok(results);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<StudentERMDTO>>> GetAllEnrollments()
        {
            var enrollments = await _service.GetAllAsync();
            return Ok( new { Results = enrollments});
        }

        [HttpGet("detail/{studentId}")]
        public async Task<IActionResult> GetDetail([FromRoute]string studentId)
        {
            var result = await _service.GetDetailAsync(studentId);

            if (result == null)
                return NotFound(new { Message = "Student not found!" });

            return Ok( new {results = result});
        }


        [HttpPost("enrollment")]
        public async Task<IActionResult> EnrollmentStudent([FromBody] StudentEnrollmentDto dto)
        {
            var sdEnroll = await _service.CreateEnrollmentAsync(dto);
            if (sdEnroll == "DUPLICATE_CODE")
                return Conflict("Student code already exists.");
            if (sdEnroll is null)
                return Conflict("Field to create enrollment.");
            return Ok(new { message = "Student enrollment successfully."});
        }
    }
}
