namespace ProductWebAPI.Models;

public class CurrentEducation
{
    public int EducationId { get; set; }   // PK
    public string StudentId { get; set; } = Guid.NewGuid().ToString();     // FK
    public string Education { get; set; } = default!;
    public string BacIIGrade { get; set; } = default!;
    public string BacIICertificateCode { get; set; } = default!;
    public int BacIIYear { get; set; }
    public string HighSchoolName { get; set; } = default!;
    public string HighSchoolLocation { get; set; } = default!;
    public string CareerType { get; set; } = default!;
    public string AcademicUnit { get; set; } = default!;

    public StudentERM Student { get; set; } = default!;
}
