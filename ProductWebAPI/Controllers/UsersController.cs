using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Data;
using ProductWebAPI.DTOs;
using ProductWebAPI.Services;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTOs>>> GetAllUsers()
        {
            // get all users
            var result = await _service.GetUsersAsync();
            return Ok(new { results = result});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO dto)
        {
            if (!ModelState.IsValid)  // validate field form dto
                return BadRequest(ModelState);
            var result = await _service.CreateUserAsync(dto);
            if (!result.success) // Handle error form
                return BadRequest(new { message = result.message });
            return Ok(new { message = result.message }); // create success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _service.DeleteUserAsync(id);
            if (!result.success)
                return NotFound(result.message);

            return Ok(result.message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserDto dto)
        {
            var user = await _service.UpdateUserAsync(id, dto);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }



    }
}
