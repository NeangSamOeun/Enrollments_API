using ProductWebAPI.DTOs;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services
{
    public interface IUserService
    {
        Task<(bool success, string message)> CreateUserAsync(CreateUserDTO user);
        Task<(bool success, string message)> DeleteUserAsync(string id);
        Task<User?> GetUserByIdAsync(string id);
        Task<List<User>> GetUsersAsync();
        Task<UserDTOs?> UpdateUserAsync(string id, UpdateUserDto userDto);
    }
}