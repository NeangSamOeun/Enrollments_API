namespace ProductWebAPI.Models;

public class Subjects
{
    public int SubjectId { get; set; }   // PK
    public int MajorId { get; set; }

    public string SubjectName { get; set; } = default!;

    public Majors Major { get; set; } = default!;
}
