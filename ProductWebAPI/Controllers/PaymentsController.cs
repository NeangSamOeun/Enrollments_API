using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.DTOs;
using ProductWebAPI.Models;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly StudentDbContext _context;
        public PaymentsController(StudentDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPayments()
        {
            return await _context.Payments.Select(p => new PaymentDTO
            {
                Id = p.Id,
                StudentId = p.StudentId,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate
            }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDTO>> CreatePayment(PaymentDTO dto)
        {
            var student = await _context.Students.FindAsync(dto.StudentId);
            if (student == null) return BadRequest("Invalid Student ID");

            var payment = new Payment
            {
                StudentId = dto.StudentId,
                Amount = dto.Amount,
                PaymentDate = dto.PaymentDate
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            dto.Id = payment.Id;
            return CreatedAtAction(nameof(GetPayments), new { id = dto.Id }, dto);
        }

    }
}
