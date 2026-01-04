using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.DTOs;
using ProductWebAPI.Services;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly IBatchService _service;
        public BatchController(IBatchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var batch = await _service.GetByIdAsync(id);
            if (batch == null) return NotFound();
            return Ok(batch);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BatchCreateDto dto)
        {
            var batch = await _service.CreateAsync(dto);
            return Ok(batch);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BatchCreateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return Ok(new {message = "Update was successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
