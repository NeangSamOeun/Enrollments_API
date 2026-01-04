using Microsoft.AspNetCore.Http.HttpResults;
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
            bool emailExists = await _repo.GetUsers()
                .AnyAsync(u => u.Email == user.Email);

            if (emailExists)
                return (false, "Email already exists");

            var entity = user.ToEntity();
            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _repo.AddUserAsync(entity);
            return (true, "User created successfully");
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            // Call the repository method
            var user = await _repo.GetUserByIdAsync(id);

            // Return null if not found
            if (user == null) return null;

            return user;
        }


        public async Task<UserDTOs?> UpdateUserAsync(string id, UpdateUserDto userDto)
        {
            var existingUser = await _repo.GetUserByIdAsync(id);
            if (existingUser == null)
                return null;

            // Update fields
            existingUser.FirstName = userDto.FirstName;
            existingUser.LastName = userDto.LastName;
            existingUser.Email = userDto.Email;
            existingUser.Role = Enum.Parse<UserRole>(userDto.Role, true);
            existingUser.Gender = userDto.Gender;

            var updatedUser = await _repo.UpdateUserAsync(id, existingUser);

            if (updatedUser == null)
                return null;

            return new UserDTOs
            {
                Id = updatedUser.Id,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Email = updatedUser.Email,
                Gender = updatedUser.Gender,
                Role = updatedUser.Role.ToString()
            };
        }

        public async Task<(bool success, string message)> DeleteUserAsync(string id)
        {
            bool deleted = await _repo.DeleteUserAsync(id);
            if (!deleted)
                return (false, "User not found");

            return (true, "User deleted successfully");
        }
    }
}
