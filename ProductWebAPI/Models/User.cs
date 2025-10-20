using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required, MaxLength(50)]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public UserRole Role { get; set; }

        public string? OtpCode { get; set; }
        public DateTime? OtpExpireTime { get; set; }
    }
}
