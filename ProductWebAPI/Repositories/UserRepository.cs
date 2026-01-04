using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.Models;

namespace ProductWebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StudentDbContext _dbContext;

        public UserRepository(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<User> GetUsers()
        {
            return _dbContext.Users.AsNoTracking();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateUserAsync(string id, User user)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (existingUser == null)
                return null;

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;
            existingUser.Gender = user.Gender;

            await _dbContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return false;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
