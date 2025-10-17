namespace ProductWebAPI.DTOs
{
    public class CourseDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CourseCode { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public int Credits { get; set; }
        public decimal Fee { get; set; }
    }
}
