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
        private readonly EmailService _emailService;
        public AuthController(StudentDbContext context, TokenService tokenService, EmailService emailService)
        {
            _context = context;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
                return Unauthorized("Invalid email or password");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
                return Unauthorized("Invalid email or password");

            //Generate 6-digit OTP
            user.OtpCode = new Random().Next(100000, 999999).ToString();

            //OTP expires in 3 minutes
            user.OtpExpireTime = DateTime.Now.AddMinutes(3);

            await _context.SaveChangesAsync();

            // Send email
            await _emailService.SendEmailAsync(
                user.Email,
                "Student Enrollment OTP Code",
                $"Your OTP is: {user.OtpCode}\nThis code will expire in 3 minutes."
            );

            return Ok(new { message = "OTP sent to your email" });
        }


        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(OtpVerifyDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.OtpCode == dto.OtpCode);
            if (user == null)
                return Unauthorized("Invalid OTP");

            //Check OTP and Expiry
            if (user.OtpExpireTime < DateTime.Now)
                return Unauthorized("Invalid or expired OTP");

            // Clear OTP after success
            user.OtpCode = null;
            user.OtpExpireTime = null;
            await _context.SaveChangesAsync();

            //Create JWT Token
            var token = _tokenService.CreateToken(user);

            return Ok(new { Token = token, Role = user.Role.ToString(), Message = "Login success" });
        }

    }
}
