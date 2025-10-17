using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;

namespace ProductWebAPI.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
           : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 🧑‍🎓 Student
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");

                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id)
                      .HasColumnType("varchar(50)")
                      .IsRequired();

                entity.Property(s => s.FullName)
                      .HasColumnType("varchar(100)")
                      .IsRequired();

                entity.Property(s => s.Gender)
                      .HasColumnType("varchar(10)");

                entity.Property(s => s.Email)
                      .HasColumnType("varchar(100)");

                // Relationships
                entity.HasMany(s => s.Enrollments)
                      .WithOne(e => e.Student)
                      .HasForeignKey(e => e.StudentId);

                entity.HasMany(s => s.Payments)
                      .WithOne(p => p.Student)
                      .HasForeignKey(p => p.StudentId);
            });

            // 📚 Course
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");

                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id)
                      .HasColumnType("varchar(50)")
                      .IsRequired();

                entity.Property(c => c.CourseCode)
                      .HasColumnType("varchar(50)")
                      .IsRequired();

                entity.Property(c => c.CourseName)
                      .HasColumnType("varchar(100)")
                      .IsRequired();

                entity.Property(c => c.Fee)
                      .HasColumnType("decimal(18,2)");

                entity.HasIndex(c => c.CourseCode).IsUnique();

                entity.HasMany(c => c.Enrollments)
                      .WithOne(e => e.Course)
                      .HasForeignKey(e => e.CourseId);
            });

            // 📝 Enrollment
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("Enrollments");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasColumnType("varchar(50)")
                      .IsRequired();

                entity.Property(e => e.EnrollDate)
                      .HasColumnType("datetime");

                entity.Property(e => e.StudentId)
                      .HasColumnType("varchar(50)")
                      .IsRequired();

                entity.Property(e => e.CourseId)
                      .HasColumnType("varchar(50)")
                      .IsRequired();
            });

            // 💰 Payment
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");

                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id)
                      .HasColumnType("varchar(50)")
                      .IsRequired();

                entity.Property(p => p.Amount)
                      .HasColumnType("decimal(18,2)");

                entity.Property(p => p.PaymentDate)
                      .HasColumnType("datetime");

                entity.Property(p => p.StudentId)
                      .HasColumnType("varchar(50)")
                      .IsRequired();
            });

            // 👤 User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id)
                      .HasColumnType("varchar(50)")
                      .IsRequired();

                entity.Property(u => u.Username)
                      .HasColumnType("varchar(50)")
                      .IsRequired();

                entity.HasIndex(u => u.Username).IsUnique();

                entity.Property(u => u.PasswordHash)
                      .HasColumnType("varchar(255)")
                      .IsRequired();

                entity.Property(u => u.Role)
                      .HasConversion<string>() // ✅ store enum as string
                      .HasColumnType("varchar(50)")
                      .IsRequired();
            });
        }
    }
}
