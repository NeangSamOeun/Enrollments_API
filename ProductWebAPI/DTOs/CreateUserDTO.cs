using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.DTOs
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "First name is require")]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Gender is required")]
        [MaxLength(10)]
        public string Gender { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Role is required")]
        [MaxLength(50)]
        public string Role { get; set; } = string.Empty;
    }
}
