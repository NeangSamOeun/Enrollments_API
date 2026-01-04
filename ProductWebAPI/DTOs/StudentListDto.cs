namespace ProductWebAPI.DTOs
{
    public class StudentListDto
    {
        public string StudentId { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Sex { get; set; } = default!;
        public DateTime DOB { get; set; }
        public string? PhoneNumber { get; set; } = default!;
        public DateTime? RegisterDate { get; set; }
        public string? Major { get; set; } = default!;
        public string? Status { get; set; } = default!;
        public int? BacIIYear { get; set; }
        public int? BatchId { get; set; }
    }
}
