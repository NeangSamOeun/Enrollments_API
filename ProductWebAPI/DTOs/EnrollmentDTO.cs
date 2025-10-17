namespace ProductWebAPI.DTOs
{
    public class EnrollmentDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string StudentId { get; set; } = string.Empty;
        public string CourseId { get; set; } = string.Empty;
        public DateTime EnrollDate { get; set; }
    }
}
