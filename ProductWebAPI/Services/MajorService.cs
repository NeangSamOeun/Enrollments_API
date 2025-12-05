using ProductWebAPI.DTOs;
using ProductWebAPI.Models;
using ProductWebAPI.Repositories;

namespace ProductWebAPI.Services
{
    public class MajorService : IMajorService
    {
        private readonly IMajorRepository _repo;

        public MajorService(IMajorRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<MajorDto>> GetAll()
        {
            var majors = await _repo.GetAllAsync();

            return majors.Select(m => new MajorDto
            {
                MajorId = m.MajorId,
                MajorName = m.MajorName
            }).ToList();
        }

        public async Task<MajorDto?> GetById(int id)
        {
            var major = await _repo.GetByIdAsync(id);
            if (major == null) return null;

            return new MajorDto
            {
                MajorId = major.MajorId,
                MajorName = major.MajorName
            };
        }

        public async Task<MajorDto> Create(MajorCreateUpdateDto dto)
        {
            var entity = new Majors
            {
                MajorName = dto.MajorName
            };

            var created = await _repo.CreateAsync(entity);

            return new MajorDto
            {
                MajorId = created.MajorId,
                MajorName = created.MajorName
            };
        }

        public async Task<MajorDto?> Update(int id, MajorCreateUpdateDto dto)
        {
            var major = await _repo.GetByIdAsync(id);
            if (major == null) return null;

            major.MajorName = dto.MajorName;

            var updated = await _repo.UpdateAsync(major);

            return new MajorDto
            {
                MajorId = updated.MajorId,
                MajorName = updated.MajorName
            };
        }

        public async Task<bool> Delete(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
