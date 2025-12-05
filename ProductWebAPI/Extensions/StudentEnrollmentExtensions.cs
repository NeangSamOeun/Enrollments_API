using ProductWebAPI.DTOs;
using ProductWebAPI.Models;

namespace ProductWebAPI.Extensions;
public static class StudentEnrollmentExtensions
{
    public static StudentERM ToERM(this StudentEnrollmentDto dto, string studentId)
        => new StudentERM
        {
            StudentId = studentId,
            Code = dto.Code,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Sex = dto.Sex,
            DOB = dto.DOB,
            Nationality = dto.Nationality,
            Telegram = dto.Telegram,
            FatherName = dto.FatherName,
            MotherName = dto.MotherName
        };

    public static CurrentEducation ToEducation(this StudentEnrollmentDto dto, string studentId)
        => new CurrentEducation
        {
            StudentId = studentId,
            Education = dto.Education,
            BacIIGrade = dto.BacIIGrade,
            BacIICertificateCode = dto.BacIICertificateCode,
            BacIIYear = dto.BacIIYear,
            HighSchoolName = dto.HighSchoolName,
            HighSchoolLocation = dto.HighSchoolLocation,
            CareerType = dto.CareerType,
            AcademicUnit = dto.AcademicUnit
        };

    public static PermanentAddress ToAddress(this StudentEnrollmentDto dto, string studentId)
        => new PermanentAddress
        {
            StudentId = studentId,
            Country = dto.Country,
            Province = dto.Province,
            District = dto.District,
            Commune = dto.Commune,
            Village = dto.Village
        };

    public static ContactInformation ToContact(this StudentEnrollmentDto dto, string studentId)
        => new ContactInformation
        {
            StudentId = studentId,
            PhoneNumber = dto.PhoneNumber,
            GuardianNumber = dto.GuardianNumber,
            EmergencyName = dto.EmergencyName,
            Relationship = dto.Relationship,
            EmergencyContact = dto.EmergencyContact,
            EmergencyWorkplace = dto.EmergencyWorkplace
        };

    public static RegisterInformation ToRegister(this StudentEnrollmentDto dto, string studentId)
        => new RegisterInformation
        {
            StudentId = studentId,
            MajorId = dto.MajorId,
            RegisterDate = dto.RegisterDate,
            RegisterType = dto.RegisterType,
            Status = dto.Status,
            Batch = dto.Batch
        };

    public static StudentERMDTO ToDto(this StudentERM erm)
        => new StudentERMDTO
        {
            //StudentId = erm.StudentId,
            Code = erm.Code,
            FirstName = erm.FirstName,
            LastName = erm.LastName,
            Sex = erm.Sex,
            DOB = erm.DOB,
            Nationality = erm.Nationality,
            Telegram = erm.Telegram,
            FatherName = erm.FatherName,
            MotherName = erm.MotherName
        };

}
