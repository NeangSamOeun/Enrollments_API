using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.Models;

namespace ProductWebAPI.Repositories
{
    public class BatchRepository : IBatchRepository
    {
        private readonly StudentDbContext _context;

        public BatchRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Batch>> GetAllAsync()
        {
            return await _context.Batches.ToListAsync();
        }

        public async Task<Batch?> GetByIdAsync(int id)
        {
            return await _context.Batches.FindAsync(id);
        }

        public async Task<Batch> AddAsync(Batch batch)
        {
            await _context.Batches.AddAsync(batch);
            await _context.SaveChangesAsync();
            return batch;
        }

        public async Task UpdateAsync(Batch batch)
        {
            _context.Batches.Update(batch);
        }

        public async Task DeleteAsync(Batch batch)
        {
            _context.Batches.Remove(batch);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
