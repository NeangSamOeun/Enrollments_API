using ProductWebAPI.Models;

namespace ProductWebAPI.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();
        Task<User> AddUserAsync(User user);
        Task<User?> UpdateUserAsync(string id, User user);
        Task<bool> DeleteUserAsync(string id);
        Task<User?> GetUserByIdAsync(string id);
    }
}
