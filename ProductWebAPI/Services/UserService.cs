using Microsoft.EntityFrameworkCore;
using ProductWebAPI.DTOs;
using ProductWebAPI.Extensions;
using ProductWebAPI.Models;
using ProductWebAPI.Repositories;

namespace ProductWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _repo.GetUsers().ToListAsync();
        }

        public async Task<(bool success, string message)> CreateUserAsync(CreateUserDTO user)
        {
            // Check duplicate email
            bool emailExists = await _repo.GetUsers().AnyAsync(u => u.Email == user.Email);
            if (emailExists)
                return (false, "Email already exists");
            // Hash password
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            // Convert DTO
            var entity = user.ToEntity();
            entity.PasswordHash = hashPassword;
            await _repo.AddUserAsync(entity);
            return (true, "User created successfully");
        }
    }
}
