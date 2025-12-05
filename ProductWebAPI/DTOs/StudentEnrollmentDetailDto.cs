namespace ProductWebAPI.DTOs
{
    public class StudentEnrollmentDetailDto
    {
        // StudentERM
        public string StudentId { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Sex { get; set; } = default!;
        public DateTime? DOB { get; set; }   // FIXED (nullable)
        public string Nationality { get; set; } = default!;
        public string Telegram { get; set; } = default!;
        public string FatherName { get; set; } = default!;
        public string MotherName { get; set; } = default!;

        // CurrentEducation
        public string Education { get; set; } = default!;
        public string BacIIGrade { get; set; } = default!;
        public string BacIICertificateCode { get; set; } = default!;
        public int BacIIYear { get; set; }
        public string HighSchoolName { get; set; } = default!;
        public string HighSchoolLocation { get; set; } = default!;
        public string CareerType { get; set; } = default!;
        public string AcademicUnit { get; set; } = default!;

        // PermanentAddress
        public string Country { get; set; } = default!;
        public string Province { get; set; } = default!;
        public string District { get; set; } = default!;
        public string Commune { get; set; } = default!;
        public string Village { get; set; } = default!;

        // ContactInformation
        public string PhoneNumber { get; set; } = default!;
        public string GuardianNumber { get; set; } = default!;
        public string EmergencyName { get; set; } = default!;
        public string Relationship { get; set; } = default!;
        public string EmergencyContact { get; set; } = default!;
        public string EmergencyWorkplace { get; set; } = default!;

        // RegisterInformation
        public int? MajorId { get; set; }    // FIXED
        public string? MajorName { get; set; }
        public DateTime? RegisterDate { get; set; }   // FIXED
        public string? RegisterType { get; set; }
        public string? Status { get; set; }
        public string? Batch { get; set; }
    }
}
