using ProductWebAPI.DTOs;
using ProductWebAPI.Models;

namespace ProductWebAPI.Extensions
{
    public static class UserExtensions
    {
        public static User ToEntity(this CreateUserDTO user)
        {
            Enum.TryParse<UserRole>(user.Role, true, out var parsedRole);
            var result = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                PasswordHash = user.Password,
                Role = parsedRole,
            };
            return result;
        }
    }
}
