namespace ProductWebAPI.Models;

public class ContactInformation
{
    public int ContactId { get; set; }     // PK
    public string StudentId { get; set; } = Guid.NewGuid().ToString();     // FK

    public string PhoneNumber { get; set; } = default!;
    public string GuardianNumber { get; set; } = default!;
    public string EmergencyName { get; set; } = default!;
    public string Relationship { get; set; } = default!;
    public string EmergencyContact { get; set; } = default!;
    public string EmergencyWorkplace { get; set; } = default!;
    public StudentERM Student { get; set; } = default!;
}
