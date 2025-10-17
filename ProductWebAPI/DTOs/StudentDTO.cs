namespace ProductWebAPI.DTOs
{
    public class StudentDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } 
        public string Email { get; set; } = string.Empty;
    }
}
