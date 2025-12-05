namespace ProductWebAPI.Models;

public class Majors
{
    public int MajorId { get; set; }     // PK
    public string MajorName { get; set; } = default!;

    public ICollection<Subjects> Subjects { get; set; } = null!;
    public ICollection<RegisterInformation> RegisterInformations { get; set; } = default!;
}
