public class StudentEnrollmentPatchDto
{
    public string StudentId { get; set; }
    public string? Code { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Sex { get; set; }
    public DateTime? DOB { get; set; }
    public string? Nationality { get; set; }
    public string? Telegram { get; set; }
    public string? FatherName { get; set; }
    public string? MotherName { get; set; }

    public string? Education { get; set; }
    public string? BacIIGrade { get; set; }
    public string? BacIICertificateCode { get; set; }
    public int? BacIIYear { get; set; }
    public string? HighSchoolName { get; set; }
    public string? HighSchoolLocation { get; set; }
    public string? CareerType { get; set; }
    public string? AcademicUnit { get; set; }

    public string? Country { get; set; }
    public string? Province { get; set; }
    public string? District { get; set; }
    public string? Commune { get; set; }
    public string? Village { get; set; }

    public string? PhoneNumber { get; set; }
    public string? GuardianNumber { get; set; }
    public string? EmergencyName { get; set; }
    public string? Relationship { get; set; }
    public string? EmergencyContact { get; set; }
    public string? EmergencyWorkplace { get; set; }

    public int? MajorId { get; set; }
    public DateTime? RegisterDate { get; set; }
    public string? RegisterType { get; set; }
    public string? Status { get; set; }
    public string? Batch { get; set; }
}
