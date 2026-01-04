using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;

namespace ProductWebAPI.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Menu> Menus { get; set; }
        // Enrollment
        public DbSet<StudentERM> StudentERMs { get; set; }
        public DbSet<CurrentEducation> CurrentEducations { get; set; }
        public DbSet<PermanentAddress> PermanentAddresses { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<Majors> Majors { get; set; }
        public DbSet<RegisterInformation> RegisterInformations { get; set; }
        public DbSet<Batch> Batches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Student
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id).HasColumnType("varchar(50)").IsRequired();
                entity.Property(s => s.FullName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(s => s.Gender).HasColumnType("varchar(10)");
                entity.Property(s => s.Email).HasColumnType("varchar(100)");

                // Relationships
                entity.HasMany(s => s.Enrollments)
                      .WithOne(e => e.Student)
                      .HasForeignKey(e => e.StudentId);
                entity.HasMany(s => s.Payments)
                      .WithOne(p => p.Student)
                      .HasForeignKey(p => p.StudentId);
            });

            //  Course
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).HasColumnType("varchar(50)").IsRequired();
                entity.Property(c => c.CourseCode).HasColumnType("varchar(50)").IsRequired();
                entity.Property(c => c.CourseName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(c => c.Fee).HasColumnType("decimal(18,2)");
                entity.HasIndex(c => c.CourseCode).IsUnique();
                entity.HasMany(c => c.Enrollments).WithOne(e => e.Course).HasForeignKey(e => e.CourseId);
            });

            // Enrollment
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("Enrollments");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType("varchar(50)").IsRequired();
                entity.Property(e => e.EnrollDate).HasColumnType("datetime");
                entity.Property(e => e.StudentId).HasColumnType("varchar(50)").IsRequired();
                entity.Property(e => e.CourseId).HasColumnType("varchar(50)").IsRequired();
            });

            // Payment
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).HasColumnType("varchar(50)").IsRequired();
                entity.Property(p => p.Amount).HasColumnType("decimal(18,2)");
                entity.Property(p => p.PaymentDate).HasColumnType("datetime");
                entity.Property(p => p.StudentId).HasColumnType("varchar(50)").IsRequired();
            });

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).HasColumnType("varchar(50)").IsRequired();
                entity.Property(f => f.FirstName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(l => l.LastName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(u => u.Email).HasColumnType("varchar(50)").IsRequired();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(g => g.Gender).HasColumnType("varchar(10)").IsRequired();
                entity.Property(u => u.PasswordHash).HasColumnType("varchar(255)").IsRequired();

                entity.Property(u => u.Role)
                      .HasConversion<string>() // store enum as string
                      .HasColumnType("varchar(50)")
                      .IsRequired();
                // ADD NEW FIELDS FOR OTP
                entity.Property(u => u.OtpCode).HasColumnType("varchar(10)").IsRequired(false);
                entity.Property(u => u.OtpExpireTime).HasColumnType("datetime").IsRequired(false);

            });

            // Add Menu
            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menus");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Title).HasColumnType("varchar(100)").IsRequired();
                entity.Property(m => m.Url).HasColumnType("varchar(100)").IsRequired();
                entity.Property(m => m.Icon).HasColumnType("varchar(50)");
                entity.Property(m => m.AllowedRoles).HasColumnType("varchar(50)").IsRequired();
            });

            // Additional configurations for Enrollment related entities
            modelBuilder.Entity<StudentERM>(entity =>
            {
                entity.ToTable("StudentEnrollment");
                entity.HasKey(m => m.StudentId);

                // assume StudentId in StudentERM is string to match Student.Id
                entity.Property(m => m.StudentId).HasColumnType("varchar(50)").IsRequired();
                entity.Property(m => m.Code).HasColumnType("varchar(50)").IsRequired();
                entity.Property(m => m.FirstName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(m => m.LastName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(m => m.Sex).HasColumnType("varchar(10)").IsRequired();
                entity.Property(m => m.DOB).HasColumnType("datetime").IsRequired();
                entity.Property(m => m.Nationality).HasColumnType("varchar(50)").IsRequired();
                entity.Property(m => m.Telegram).HasColumnType("varchar(50)").IsRequired();
                entity.Property(m => m.FatherName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(m => m.MotherName).HasColumnType("varchar(100)").IsRequired();

                // entity.HasOne(m => m.Student).WithOne(s => s.StudentERM).HasForeignKey<StudentERM>(m => m.StudentId);
            });

            // CurrentEducation entity configuration
            modelBuilder.Entity<CurrentEducation>(entity =>
            {
                entity.ToTable("CurrentEducation");
                entity.HasKey(c => c.StudentId);

                entity.Property(c => c.StudentId).HasColumnType("varchar(50)").IsRequired();
                entity.Property(c => c.Education).HasColumnType("varchar(100)").IsRequired();
                entity.Property(c => c.BacIIGrade).HasColumnType("char(2)").IsRequired();
                entity.Property(c => c.BacIICertificateCode).HasColumnType("varchar(50)").IsRequired();
                entity.Property(c => c.BacIIYear).HasColumnType("int").IsRequired();
                entity.Property(c => c.HighSchoolName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(c => c.HighSchoolLocation).HasColumnType("varchar(100)").IsRequired();
                entity.Property(c => c.CareerType).HasColumnType("varchar(50)").IsRequired();
                entity.Property(c => c.AcademicUnit).HasColumnType("varchar(50)");

                entity.HasOne(c => c.Student)
                      .WithOne(s => s.CurrentEducation)
                      .HasForeignKey<CurrentEducation>(c => c.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            // PermanentAddress entity configuration
            modelBuilder.Entity<PermanentAddress>(entity =>
            {
                entity.ToTable("PermanentAddress");
                entity.HasKey(p => p.AddressId);

                entity.Property(p => p.AddressId).HasColumnType("int").IsRequired(); // PK
                                                                                     // Match Student.Id
                entity.Property(p => p.StudentId).HasColumnType("varchar(50)").IsRequired();
                entity.Property(p => p.Country).HasColumnType("varchar(50)").IsRequired();
                entity.Property(p => p.Province).HasColumnType("varchar(50)").IsRequired();
                entity.Property(p => p.District).HasColumnType("varchar(50)").IsRequired();
                entity.Property(p => p.Commune).HasColumnType("varchar(50)").IsRequired();
                entity.Property(p => p.Village).HasColumnType("varchar(50)").IsRequired();

                entity.HasOne(p => p.Student)
                      .WithOne(s => s.PermanentAddress)
                      .HasForeignKey<PermanentAddress>(p => p.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ContactInformation entity configuration
            modelBuilder.Entity<ContactInformation>(entity =>
            {
                entity.ToTable("ContactInformation");
                entity.HasKey(c => c.ContactId);

                entity.Property(c => c.ContactId).HasColumnType("int").IsRequired(); // PK
                                                                                     // Match Student.Id
                entity.Property(c => c.StudentId).HasColumnType("varchar(50)").IsRequired();

                // Phone numbers as varchar
                entity.Property(c => c.PhoneNumber).HasColumnType("varchar(20)").IsRequired();
                entity.Property(c => c.GuardianNumber).HasColumnType("varchar(20)").IsRequired();
                entity.Property(c => c.EmergencyName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(c => c.Relationship).HasColumnType("varchar(50)").IsRequired();
                entity.Property(c => c.EmergencyContact).HasColumnType("varchar(20)").IsRequired();
                entity.Property(c => c.EmergencyWorkplace).HasColumnType("varchar(100)");

                entity.HasOne(c => c.Student)
                      .WithOne(s => s.ContactInformation)
                      .HasForeignKey<ContactInformation>(c => c.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // RegisterInformation entity configuration
            modelBuilder.Entity<RegisterInformation>(entity =>
            {
                entity.ToTable("RegisterInformation");
                entity.HasKey(r => r.RegisterId);

                entity.Property(r => r.RegisterId).HasColumnType("int").IsRequired(); // PK
                entity.Property(r => r.StudentId).HasColumnType("varchar(50)").IsRequired(); // match Student.Id
                entity.Property(r => r.MajorId).HasColumnType("int").IsRequired();
                entity.Property(r => r.BatchId).HasColumnType("int").IsRequired();
                entity.Property(r => r.RegisterDate).HasColumnType("datetime").IsRequired();
                entity.Property(r => r.RegisterType).HasColumnType("varchar(50)").IsRequired();
                entity.Property(r => r.Status).HasColumnType("varchar(50)").IsRequired();

                entity.HasOne(r => r.Student)
                      .WithMany(s => s.RegisterInformations)
                      .HasForeignKey(r => r.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Major)
                      .WithMany(m => m.RegisterInformations)
                      .HasForeignKey(r => r.MajorId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Batchs)
                      .WithMany(b => b.RegisterInformation)
                      .HasForeignKey(r => r.BatchId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Majors entity configuration
            modelBuilder.Entity<Majors>(entity =>
            {
                entity.ToTable("Majors");
                entity.HasKey(m => m.MajorId);
                entity.Property(m => m.MajorName).HasColumnType("varchar(100)").IsRequired();
            });

            // batch entity configuration 
            modelBuilder.Entity<Batch>(entity =>
            {
                entity.ToTable("Batch");
                entity.HasKey(b => b.BatchId);
                entity.Property(b => b.BatchId)
                      .ValueGeneratedOnAdd(); // important for identity
                entity.Property(b => b.BatchName)
                      .HasColumnType("varchar(50)")
                      .IsRequired();
            });

        }
    }
}
