using ProductWebAPI.DTOs;
using ProductWebAPI.Models;
using ProductWebAPI.Repositories;

namespace ProductWebAPI.Services
{
    public class BatchService : IBatchService
    {
        private readonly IBatchRepository _repository;

        public BatchService(IBatchRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BatchCreateDto>> GetAllAsync()
        {
            var batches = await _repository.GetAllAsync();

            return batches.Select(b => new BatchCreateDto
            {
                BatchId = b.BatchId,
                BatchName = b.BatchName
            });
        }

        public async Task<BatchCreateDto?> GetByIdAsync(int id)
        {
            var batch = await _repository.GetByIdAsync(id);
            if (batch == null) return null;

            return new BatchCreateDto
            {
                BatchId = batch.BatchId,
                BatchName = batch.BatchName
            };
        }

        public async Task<BatchCreateDto> CreateAsync(BatchCreateDto dto)
        {
            var batch = new Batch
            {
                BatchName = dto.BatchName
            };

            await _repository.AddAsync(batch);

            return new BatchCreateDto
            {
                BatchName = batch.BatchName
            };
        }

        public async Task<bool> UpdateAsync(int id, BatchCreateDto dto)
        {
            var batch = await _repository.GetByIdAsync(id);
            if (batch == null) return false;

            batch.BatchName = dto.BatchName;

            await _repository.UpdateAsync(batch);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var batch = await _repository.GetByIdAsync(id);
            if (batch == null) return false;

            await _repository.DeleteAsync(batch);
            return await _repository.SaveChangesAsync();
        }
    }
}

