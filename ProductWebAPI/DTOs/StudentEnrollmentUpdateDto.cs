using ProductWebAPI.Models;

namespace ProductWebAPI.DTOs
{
    public class StudentEnrollmentUpdateDto
    {
        public StudentERM Student { get; set; }
        public CurrentEducation Education { get; set; }
        public PermanentAddress Address { get; set; }
        public ContactInformation Contact { get; set; }
        public RegisterInformation Register { get; set; }
    }
}
