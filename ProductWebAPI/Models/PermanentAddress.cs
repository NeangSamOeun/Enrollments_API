namespace ProductWebAPI.Models;

public class PermanentAddress
{
    public int AddressId { get; set; }      // PK
    public string StudentId { get; set; } = Guid.NewGuid().ToString();      // FK
    public string Country { get; set; } = default!;
    public string Province { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Commune { get; set; } = default!;
    public string Village { get; set; } = default!;

    public StudentERM Student { get; set; } = null!;
}
