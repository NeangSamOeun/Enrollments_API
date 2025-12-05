using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.DTOs;
using ProductWebAPI.Services;

namespace ProductWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MajorsController : ControllerBase
    {
        private readonly IMajorService _service;

        public MajorsController(IMajorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _service.GetAll();
            return Ok(new { results = results });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetById(id);
            return result == null ? NotFound() : Ok(new {results = result });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MajorCreateUpdateDto dto)
        {
            var created = await _service.Create(dto);
            return CreatedAtAction(nameof(Get), new { id = created.MajorId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MajorCreateUpdateDto dto)
        {
            var updated = await _service.Update(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);
            return deleted ? Ok(new { message = "Deleted successfully" }) : NotFound();
        }
    }
}
