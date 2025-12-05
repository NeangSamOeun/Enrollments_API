using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.DTOs;
using ProductWebAPI.Models;

namespace ProductWebAPI.Repositories
{
    public class StudentEnrollmentRepository : IStudentEnrollmentRepository
    {
        private readonly StudentDbContext _db;
        public StudentEnrollmentRepository(StudentDbContext db)
        {
            _db = db;
        }

        // get all student enrollments
        public async Task<PagedResult<StudentListDto>> GetAllWithDtoAsync(int page, int pageSize, string? search)
        {
            var query =
            from erm in _db.StudentERMs.AsNoTracking()

            join edu in _db.CurrentEducations.AsNoTracking()
                on erm.StudentId equals edu.StudentId into eduJoin
            from edu in eduJoin.DefaultIfEmpty()

            join contact in _db.ContactInformations.AsNoTracking()
                on erm.StudentId equals contact.StudentId into contactJoin
            from contact in contactJoin.DefaultIfEmpty()

            join register in _db.RegisterInformations.AsNoTracking()
                on erm.StudentId equals register.StudentId into registerJoin
            from register in registerJoin.DefaultIfEmpty()

            join major in _db.Majors.AsNoTracking()
                on register.MajorId equals major.MajorId into majorJoin
            from major in majorJoin.DefaultIfEmpty()

            select new StudentListDto
            {
                StudentId = erm.StudentId,
                Code = erm.Code,
                FirstName = erm.FirstName,
                LastName = erm.LastName,
                Sex = erm.Sex,
                DOB = erm.DOB,
                PhoneNumber = contact != null ? contact.PhoneNumber : null,
                RegisterDate = register != null ? register.RegisterDate : null,
                Major = major != null ? major.MajorName : null,
                Status = register != null ? register.Status : null,
                BacIIYear = edu != null ? edu.BacIIYear : null,
                Batch = register != null ? register.Batch : null
            };

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(x =>
                    (x.Code != null && x.Code.ToLower().Contains(search)) ||
                    (x.FirstName != null && x.FirstName.ToLower().Contains(search)) ||
                    (x.LastName != null && x.LastName.ToLower().Contains(search)) ||
                    (x.Major != null && x.Major.ToLower().Contains(search)) ||
                    (x.Status != null && x.Status.ToLower().Contains(search)) ||
                    (x.Batch != null && x.Batch.ToLower().Contains(search)) ||
                    (x.PhoneNumber != null && x.PhoneNumber.ToLower().Contains(search))
                );
            }

            var total = await query.CountAsync();
            var items = await query
                .OrderBy(x => x.Code)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<StudentListDto>
            {
                TotalRecords = total,
                Page = page,
                PageSize = pageSize,
                Items = items
            };
        }

        // get detail
        public async Task<StudentEnrollmentDetailDto?> GetStudentDetailAsync(string studentId)
        {
            var query =
                await (from s in _db.StudentERMs.AsNoTracking()
                       where s.StudentId == studentId

                       join edu in _db.CurrentEducations.AsNoTracking()
                           on s.StudentId equals edu.StudentId into eduJoin
                       from edu in eduJoin.DefaultIfEmpty()

                       join pa in _db.PermanentAddresses.AsNoTracking()
                           on s.StudentId equals pa.StudentId into paJoin
                       from pa in paJoin.DefaultIfEmpty()

                       join contact in _db.ContactInformations.AsNoTracking()
                           on s.StudentId equals contact.StudentId into contactJoin
                       from contact in contactJoin.DefaultIfEmpty()

                       join reg in _db.RegisterInformations.AsNoTracking()
                           on s.StudentId equals reg.StudentId into regJoin
                       from reg in regJoin.DefaultIfEmpty()

                       join m in _db.Majors.AsNoTracking()
                           on reg.MajorId equals m.MajorId into majorJoin
                       from m in majorJoin.DefaultIfEmpty()

                       select new StudentEnrollmentDetailDto
                       {
                           // Student
                           StudentId = s.StudentId,
                           Code = s.Code,
                           FirstName = s.FirstName,
                           LastName = s.LastName,
                           Sex = s.Sex,
                           DOB = s.DOB,                      // nullable works
                           Nationality = s.Nationality,
                           Telegram = s.Telegram,
                           FatherName = s.FatherName,
                           MotherName = s.MotherName,

                           // Education
                           Education = edu.Education,
                           BacIIGrade = edu.BacIIGrade,
                           BacIICertificateCode = edu.BacIICertificateCode,
                           BacIIYear = edu.BacIIYear,
                           HighSchoolName = edu.HighSchoolName,
                           HighSchoolLocation = edu.HighSchoolLocation,
                           CareerType = edu.CareerType,
                           AcademicUnit = edu.AcademicUnit,

                           // Permanent Address
                           Country = pa.Country,
                           Province = pa.Province,
                           District = pa.District,
                           Commune = pa.Commune,
                           Village = pa.Village,

                           // Contact
                           PhoneNumber = contact.PhoneNumber,
                           GuardianNumber = contact.GuardianNumber,
                           EmergencyName = contact.EmergencyName,
                           Relationship = contact.Relationship,
                           EmergencyContact = contact.EmergencyContact,
                           EmergencyWorkplace = contact.EmergencyWorkplace,

                           // Register
                           MajorId = reg.MajorId,
                           MajorName = m.MajorName,
                           RegisterDate = reg.RegisterDate,       // nullable
                           RegisterType = reg.RegisterType,
                           Status = reg.Status,
                           Batch = reg.Batch
                       })
                       .FirstOrDefaultAsync();

            return query;
        }


        public async Task<string> CreateStudentEnrollmentAsync(
            StudentERM erm,
            CurrentEducation edu,
            PermanentAddress address,
            ContactInformation contact,
            RegisterInformation register)
        {
            using var trx = await _db.Database.BeginTransactionAsync();
            try
            {
                // Insert StudentERM
                _db.StudentERMs.Add(erm);
                await _db.SaveChangesAsync();

                // Insert Child Entities
                _db.CurrentEducations.Add(edu);
                _db.PermanentAddresses.Add(address);
                _db.ContactInformations.Add(contact);
                _db.RegisterInformations.Add(register);
                await _db.SaveChangesAsync();
                await trx.CommitAsync();

                return erm.StudentId;
            }
            catch (Exception ex)
            {
                await trx.RollbackAsync();
                return null;
            }
        }

        public async Task<List<StudentERM>> GetAllAsync()
        {
            var results = await _db.StudentERMs.AsQueryable().ToListAsync();
            return results;
        }

        public async Task<bool> ExistsStudentIdAsync(string studentId)
        {
            return await _db.StudentERMs.AnyAsync(s => s.StudentId == studentId);
        }
        public async Task<bool> ExistsCodeAsync(string code)
        {
            return await _db.StudentERMs.AnyAsync(c => c.Code == code);
        }


    }
}
