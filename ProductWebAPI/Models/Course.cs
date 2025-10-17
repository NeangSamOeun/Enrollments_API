using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.Models
{
    public class Course
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string CourseCode { get; set; } = string.Empty;
        [Required]
        public string CourseName { get; set; } = string.Empty;
        public int Credits { get; set; }
        public decimal Fee { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
