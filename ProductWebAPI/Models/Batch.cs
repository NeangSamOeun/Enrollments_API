namespace ProductWebAPI.Models
{
    public class Batch
    {
        public int BatchId { get; set; }    // Pk
        public string BatchName { get; set; } = default!;
        public ICollection<RegisterInformation> RegisterInformation { get; set; }

    }
}
