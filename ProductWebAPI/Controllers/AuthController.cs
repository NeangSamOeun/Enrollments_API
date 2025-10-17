using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.DTOs;
using ProductWebAPI.Services;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly StudentDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(StudentDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null || user.PasswordHash != dto.Password)
                return Unauthorized("Invalid username or password");

            var token = _tokenService.CreateToken(user);

            return new LoginResponseDTO { Token = token, Role = user.Role.ToString() };
        }
    }
}
