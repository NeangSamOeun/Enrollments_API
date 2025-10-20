using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.DTOs;
using ProductWebAPI.Models;
using System.ComponentModel.DataAnnotations;

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
                Email = u.Email,
                Role = u.Role.ToString()
            }).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<UserDTOs>> CreateUser(CreateUserDTO dto)
        {
            // Validate Role
            if (!Enum.TryParse<UserRole>(dto.Role, true, out var role))
            {
                return BadRequest("Invalid role. Valid roles are: Admin, Staff, Teacher, Student");
            }

            // Validate Email Format
            if (!new EmailAddressAttribute().IsValid(dto.Email))
            {
                return BadRequest(new { message = "Invalid email format" });
            }

            // Check duplicate email
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());

            if (existingUser != null)
            {
                return BadRequest(new { message = "Email already exists" });
            }

            // Hash password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = passwordHash,
                Role = role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new UserDTOs
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString()
            });
        }


    }
}
