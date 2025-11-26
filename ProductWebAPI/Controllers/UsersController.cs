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

    }
}
