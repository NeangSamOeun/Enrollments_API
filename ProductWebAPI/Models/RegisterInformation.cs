namespace ProductWebAPI.Models;

public class RegisterInformation
{
    public int RegisterId { get; set; }  // PK
    public string StudentId { get; set; } = Guid.NewGuid().ToString();   // FK
    public int MajorId { get; set; }     // FK
    public int BatchId { get; set; }     // FK
    public DateTime RegisterDate { get; set; }
    public string RegisterType { get; set; } = default!;
    public string Status { get; set; } = default!;   // Pending / Approved
    public StudentERM Student { get; set; } = null!;
    public Majors Major { get; set; } = null!;
    public Batch Batchs { get; set; } = null!;
}
