using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.Models
{
    public class Student
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
