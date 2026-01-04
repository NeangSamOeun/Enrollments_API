using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ProductWebAPI.DTOs;
using ProductWebAPI.Models;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var student = await _service.GetByIdAsync(id);

            if (student == null)
                return NotFound(new { message = "Student not found" });

            return Ok(student);
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

        [HttpPatch("{studentId}")]
        public async Task<IActionResult> UpdateStudentPartial(string studentId, [FromBody] StudentEnrollmentPatchDto dto)
        {
            if (studentId != dto.StudentId)
                return BadRequest("Student ID mismatch");

            var success = await _service.UpdateStudentPartialAsync(dto);
            if (!success)
                return NotFound("Student not found or update failed");

            return Ok("Student updated successfully");
        }


        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent(string studentId)
        {
            var result = await _service.DeleteStudentAsync(studentId);

            if (!result)
                return NotFound(new { message = "Student not found." });

            return Ok(new { message = "Student deleted successfully." });
        }

        // update status
        [HttpPatch("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.StudentId) || string.IsNullOrEmpty(dto.Status))
                return BadRequest("Invalid request data.");

            var result = await _service.UpdateStatusAsync(dto);

            if (!result)
                return NotFound("Student not found.");

            return Ok(new { message = "Status updated successfully." });
        }



        [HttpGet("export")]
        public async Task<IActionResult> ExportToExcel([FromQuery] string? search)
        {
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var students = await _service.GetListAsync(1, int.MaxValue, search); // get all students

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Students");

            // Add headers
            worksheet.Cells[1, 1].Value = "Code";
            worksheet.Cells[1, 2].Value = "First Name";
            worksheet.Cells[1, 3].Value = "Last Name";
            worksheet.Cells[1, 4].Value = "Sex";
            worksheet.Cells[1, 5].Value = "DOB";
            worksheet.Cells[1, 6].Value = "Phone Number";
            worksheet.Cells[1, 7].Value = "Major";
            worksheet.Cells[1, 8].Value = "Register Date";
            worksheet.Cells[1, 9].Value = "Status";
            worksheet.Cells[1, 10].Value = "Batch";

            // Add data
            int row = 2;
            foreach (var s in students.Items)
            {
                worksheet.Cells[row, 1].Value = s.Code;
                worksheet.Cells[row, 2].Value = s.FirstName;
                worksheet.Cells[row, 3].Value = s.LastName;
                worksheet.Cells[row, 4].Value = s.Sex;
                worksheet.Cells[row, 5].Value = s.DOB.ToString("yyyy-MM-dd");
                worksheet.Cells[row, 6].Value = s.PhoneNumber;
                worksheet.Cells[row, 7].Value = s.Major;
                worksheet.Cells[row, 8].Value = s.RegisterDate?.ToString("yyyy-MM-dd");
                worksheet.Cells[row, 9].Value = s.Status;
                worksheet.Cells[row, 10].Value = s.BatchId;
                row++;
            }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            var fileBytes = package.GetAsByteArray();
            var fileName = $"Students_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

    }
}
