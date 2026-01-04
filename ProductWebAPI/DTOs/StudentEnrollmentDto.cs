
using ProductWebAPI.Atributes;
using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.DTOs
{
    public class StudentEnrollmentDto
    {
        [NonEmptyTrimmed]
        public string StudentId { get; set; } = Guid.NewGuid().ToString();
        [NonEmptyTrimmed, StringLength(50)]
        public string Code { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(100)]
        public string FirstName { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(100)]
        public string LastName { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(10)]
        public string Sex { get; set; } = default!;
        public DateTime DOB { get; set; }
        [NonEmptyTrimmed, StringLength(50)]
        public string Nationality { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(50)]
        public string Telegram { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(100)]
        public string FatherName { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(100)]
        public string MotherName { get; set; } = default!;

        // CurrentEducation
        [NonEmptyTrimmed, StringLength(100)]
        public string Education { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(2)]
        public string BacIIGrade { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(50)]
        public string BacIICertificateCode { get; set; } = default!;
        [Required]
        public int BacIIYear { get; set; }
        [NonEmptyTrimmed, StringLength(100)]
        public string HighSchoolName { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(100)]
        public string HighSchoolLocation { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(50)]
        public string CareerType { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(50)]
        public string AcademicUnit { get; set; } = default!;
        // PermanentAddress
        [NonEmptyTrimmed, StringLength(50)]
        public string Country { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(50)]
        public string Province { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(50)]
        public string District { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(50)]
        public string Commune { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(50)]
        public string Village { get; set; } = default!;

        // ContactInformation
        [NonEmptyTrimmed, StringLength(20)]
        public string PhoneNumber { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(20)]
        public string GuardianNumber { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(100)]
        public string EmergencyName { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(50)]
        public string Relationship { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(20)]
        public string EmergencyContact { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(100)]
        public string EmergencyWorkplace { get; set; } = default!;

        // RegisterInformation
        public int MajorId { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [NonEmptyTrimmed, StringLength(50)]
        public string RegisterType { get; set; } = default!;
        [NonEmptyTrimmed, StringLength(50)]
        public string Status { get; set; } = default!;
        [Required]
        public int BatchId { get; set; }

    }
}
