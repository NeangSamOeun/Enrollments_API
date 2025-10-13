using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Data;
using ProductWebAPI.DTOs;
using ProductWebAPI.Models;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDataDbContext _db;
        public ProductsController(ProductDataDbContext db)
        {
            _db = db;
        }
        private string getNextId()
        {
            return Guid.NewGuid().ToString();
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(ProductCreate product)
        {
            var newProduct = new Product()
            {
                Id = getNextId(),
                Code = product.Code,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
            };
            _db.Products.Add(newProduct);
            await _db.SaveChangesAsync();
            return Ok(newProduct);
        }
    }
}
