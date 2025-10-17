using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.Models
{
    public class Payment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
