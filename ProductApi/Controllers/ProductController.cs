using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.DTO;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get all Products
        [HttpGet]
        public async Task<ActionResult<List<ProductReadDto>>> GetProducts()
        {
            var products = await _context.Products
                .AsNoTracking()
                .Select(p => new ProductReadDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price
                })
                .ToListAsync();

            return Ok(products);
        }
        // Get single product by id
        [HttpGet("({id:int})")]
        public async Task<ActionResult<ProductReadDto>> GetProduct(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new ProductReadDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        //Create single product
        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> CreateProduct([FromBody] ProductCreateDto dto)
        {
            var product = new Product
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var readDto = new ProductReadDto
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price
            };
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, readDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto dto, int id)
        {
            var currentProduct = await _context.Products.FindAsync(id);
            if (currentProduct == null)
            {
                return NotFound();
            }
            currentProduct.Description = dto.Description;
            currentProduct.Price = dto.Price;
            currentProduct.Title = dto.Title;
            await _context.SaveChangesAsync();
            return Ok(currentProduct);
        }

        // Delete Single Product by ID
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok();
        }



    }
}