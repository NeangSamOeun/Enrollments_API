namespace ProductWebAPI.DTOs
{
    public class BatchCreateDto
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; } = default!;
    }
}
