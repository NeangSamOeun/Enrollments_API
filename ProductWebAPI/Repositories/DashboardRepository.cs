using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.Models.Dashboard;

public class DashboardRepository : IDashboardRepository
{
    private readonly StudentDbContext _context;

    public DashboardRepository(StudentDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> GetDashboardAsync()
    {
        return new DashboardDto
        {
            TotalStudents = await _context.Students.CountAsync(),
            TotalCourses = await _context.Courses.CountAsync(),
            TotalEnrollments = await _context.RegisterInformations.CountAsync(),
            TotalUsers = await _context.Users.CountAsync(),
            TotalMajors = await _context.Majors.CountAsync(),
            TotalBatches = await _context.Batches.CountAsync()
        };
    }
}
