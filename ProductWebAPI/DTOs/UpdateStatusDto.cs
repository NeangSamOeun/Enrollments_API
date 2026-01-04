namespace ProductWebAPI.DTOs
{
    public class UpdateStatusDto
    {
        public string StudentId { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}
