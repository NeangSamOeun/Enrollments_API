using ProductWebAPI.Models;

namespace ProductWebAPI.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();
        Task<User> AddUserAsync(User user);
    }
}