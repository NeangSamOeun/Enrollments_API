using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.DTOs;
using ProductWebAPI.Models;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly StudentDbContext _context;
        public UsersController(StudentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTOs>>> GetUsers()
        {
            return await _context.Users.Select(u => new UserDTOs
            {
                Id = u.Id,
                Username = u.Username,
                Role = u.Role.ToString()
            }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTOs>> CreateUser(CreateUserDTO dto)
        {
            // check if role is valid
            if (!Enum.TryParse<UserRole>(dto.Role, true, out var role))
            {
                return BadRequest("Invalid role. Valid roles are: Admin, Staff, Teacher, Student");
            }

            // check if for duplicate username
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == dto.Username.ToLower());
            if (existingUser != null)
            {
                return BadRequest(new { message = "username already exists" });
            }

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = dto.Password,
                Role = role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTOs
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role.ToString()
            };
        }

    }
}
