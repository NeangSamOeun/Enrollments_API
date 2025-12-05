using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.Models;

namespace ProductWebAPI.Repositories
{
    public class MajorRepository : IMajorRepository
    {
        private readonly StudentDbContext _context;

        public MajorRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<List<Majors>> GetAllAsync()
        {
            return await _context.Majors.ToListAsync();
        }

        public async Task<Majors?> GetByIdAsync(int id)
        {
            return await _context.Majors.FirstOrDefaultAsync(x => x.MajorId == id);
        }

        public async Task<Majors> CreateAsync(Majors major)
        {
            _context.Majors.Add(major);
            await _context.SaveChangesAsync();
            return major;
        }

        public async Task<Majors> UpdateAsync(Majors major)
        {
            _context.Majors.Update(major);
            await _context.SaveChangesAsync();
            return major;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Majors.FindAsync(id);
            if (entity == null) return false;

            _context.Majors.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
