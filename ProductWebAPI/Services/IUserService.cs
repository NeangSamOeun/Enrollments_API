using ProductWebAPI.DTOs;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services
{
    public interface IUserService
    {
        Task<(bool success, string message)> CreateUserAsync(CreateUserDTO user);
        Task<List<User>> GetUsersAsync();
    }
}