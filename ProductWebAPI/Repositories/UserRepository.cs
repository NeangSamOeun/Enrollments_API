using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.DTOs;
using ProductWebAPI.Models;

namespace ProductWebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StudentDbContext _dbContext;

        public UserRepository(StudentDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IQueryable<User> GetUsers()
        {
            var users = _dbContext.Users.AsQueryable();
            return users;
        }

        public async Task<User> AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
