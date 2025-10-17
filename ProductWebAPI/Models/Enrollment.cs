using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.Models
{
    public class Enrollment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string StudentId { get; set; } = string.Empty;
        public Student Student { get; set; } = null!;
        public string CourseId { get; set; } = string.Empty;
        public Course Course { get; set; } = null!;
        public DateTime EnrollDate { get; set; }
    }
}
