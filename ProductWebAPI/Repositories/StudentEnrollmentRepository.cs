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
                    PhoneNumber = contact!.PhoneNumber,
                    RegisterDate = register!.RegisterDate,
                    Major = major!.MajorName,
                    Status = register!.Status,
                    BacIIYear = edu!.BacIIYear,
                    BatchId = register.BatchId
                };

            //FIXED SEARCH — REMOVED Batch AND REMOVED COMPLEX NEW StudentListDto() IN WHERE
            if (!string.IsNullOrWhiteSpace(search))
            {
                string term = search.ToLower();

                query = query.Where(x =>
                    (x.Code ?? "").ToLower().Contains(term) ||
                    (x.FirstName ?? "").ToLower().Contains(term) ||
                    (x.LastName ?? "").ToLower().Contains(term) ||
                    (x.Major ?? "").ToLower().Contains(term) ||
                    (x.Status ?? "").ToLower().Contains(term) ||
                    (x.PhoneNumber ?? "").ToLower().Contains(term)
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
        // get student by id (basic entity)
        public async Task<StudentERM?> GetByIdAsync(string studentId)
        {
            return await _db.StudentERMs
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StudentId == studentId);
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
                           BatchId = reg.BatchId
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

        // update student
        public async Task<bool> UpdateStudentPartialAsync(StudentEnrollmentPatchDto dto)
        {
            using var trx = await _db.Database.BeginTransactionAsync();
            try
            {
                var student = await _db.StudentERMs.FirstOrDefaultAsync(s => s.StudentId == dto.StudentId);
                if (student == null) return false;
                // Update only non-null fields
                if (!string.IsNullOrEmpty(dto.Code)) student.Code = dto.Code;
                if (!string.IsNullOrEmpty(dto.FirstName)) student.FirstName = dto.FirstName;
                if (!string.IsNullOrEmpty(dto.LastName)) student.LastName = dto.LastName;
                if (!string.IsNullOrEmpty(dto.Sex)) student.Sex = dto.Sex;
                if (dto.DOB.HasValue) student.DOB = dto.DOB.Value;
                if (!string.IsNullOrEmpty(dto.Nationality)) student.Nationality = dto.Nationality;
                if (!string.IsNullOrEmpty(dto.Telegram)) student.Telegram = dto.Telegram;
                if (!string.IsNullOrEmpty(dto.FatherName)) student.FatherName = dto.FatherName;
                if (!string.IsNullOrEmpty(dto.MotherName)) student.MotherName = dto.MotherName;
                _db.StudentERMs.Update(student);

                // Education
                var edu = await _db.CurrentEducations.FirstOrDefaultAsync(e => e.StudentId == dto.StudentId);
                if (edu != null)
                {
                    if (!string.IsNullOrEmpty(dto.Education)) edu.Education = dto.Education;
                    if (!string.IsNullOrEmpty(dto.BacIIGrade)) edu.BacIIGrade = dto.BacIIGrade;
                    if (!string.IsNullOrEmpty(dto.BacIICertificateCode)) edu.BacIICertificateCode = dto.BacIICertificateCode;
                    if (dto.BacIIYear.HasValue) edu.BacIIYear = dto.BacIIYear.Value;
                    if (!string.IsNullOrEmpty(dto.HighSchoolName)) edu.HighSchoolName = dto.HighSchoolName;
                    if (!string.IsNullOrEmpty(dto.HighSchoolLocation)) edu.HighSchoolLocation = dto.HighSchoolLocation;
                    if (!string.IsNullOrEmpty(dto.CareerType)) edu.CareerType = dto.CareerType;
                    if (!string.IsNullOrEmpty(dto.AcademicUnit)) edu.AcademicUnit = dto.AcademicUnit;
                    _db.CurrentEducations.Update(edu);
                }

                // Address
                var address = await _db.PermanentAddresses.FirstOrDefaultAsync(a => a.StudentId == dto.StudentId);
                if (address != null)
                {
                    if (!string.IsNullOrEmpty(dto.Country)) address.Country = dto.Country;
                    if (!string.IsNullOrEmpty(dto.Province)) address.Province = dto.Province;
                    if (!string.IsNullOrEmpty(dto.District)) address.District = dto.District;
                    if (!string.IsNullOrEmpty(dto.Commune)) address.Commune = dto.Commune;
                    if (!string.IsNullOrEmpty(dto.Village)) address.Village = dto.Village;
                    _db.PermanentAddresses.Update(address);
                }

                // Contact
                var contact = await _db.ContactInformations.FirstOrDefaultAsync(c => c.StudentId == dto.StudentId);
                if (contact != null)
                {
                    if (!string.IsNullOrEmpty(dto.PhoneNumber)) contact.PhoneNumber = dto.PhoneNumber;
                    if (!string.IsNullOrEmpty(dto.GuardianNumber)) contact.GuardianNumber = dto.GuardianNumber;
                    if (!string.IsNullOrEmpty(dto.EmergencyName)) contact.EmergencyName = dto.EmergencyName;
                    if (!string.IsNullOrEmpty(dto.Relationship)) contact.Relationship = dto.Relationship;
                    if (!string.IsNullOrEmpty(dto.EmergencyContact)) contact.EmergencyContact = dto.EmergencyContact;
                    if (!string.IsNullOrEmpty(dto.EmergencyWorkplace)) contact.EmergencyWorkplace = dto.EmergencyWorkplace;
                    _db.ContactInformations.Update(contact);
                }

                // Register
                var register = await _db.RegisterInformations.FirstOrDefaultAsync(r => r.StudentId == dto.StudentId);
                if (register != null)
                {
                    if (dto.MajorId.HasValue) register.MajorId = dto.MajorId.Value;
                    if (dto.RegisterDate.HasValue) register.RegisterDate = dto.RegisterDate.Value;
                    if (!string.IsNullOrEmpty(dto.RegisterType)) register.RegisterType = dto.RegisterType;
                    if (!string.IsNullOrEmpty(dto.Status)) register.Status = dto.Status;
                    _db.RegisterInformations.Update(register);
                }

                await _db.SaveChangesAsync();
                await trx.CommitAsync();
                return true;
            }
            catch
            {
                await trx.RollbackAsync();
                return false;
            }
        }

        // delete student 
        public async Task<bool> DeleteStudentAsync(string studentId)
        {
            using var trx = await _db.Database.BeginTransactionAsync();
            try
            {
                var student = await _db.StudentERMs
                    .FirstOrDefaultAsync(s => s.StudentId == studentId);

                if (student == null)
                    return false;
                // Remove child entities
                var register = _db.RegisterInformations.Where(r => r.StudentId == studentId);
                var contact = _db.ContactInformations.Where(c => c.StudentId == studentId);
                var address = _db.PermanentAddresses.Where(p => p.StudentId == studentId);
                var education = _db.CurrentEducations.Where(e => e.StudentId == studentId);

                _db.RegisterInformations.RemoveRange(register);
                _db.ContactInformations.RemoveRange(contact);
                _db.PermanentAddresses.RemoveRange(address);
                _db.CurrentEducations.RemoveRange(education);

                _db.StudentERMs.Remove(student);

                await _db.SaveChangesAsync();
                await trx.CommitAsync();

                return true;
            }
            catch
            {
                await trx.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> UpdateStatusAsync(string studentId, string status)
        {
            var register = await _db.RegisterInformations
                .FirstOrDefaultAsync(r => r.StudentId == studentId);

            if (register == null)
                return false;

            register.Status = status;

            _db.RegisterInformations.Update(register);
            await _db.SaveChangesAsync();

            return true;
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
