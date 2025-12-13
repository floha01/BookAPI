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
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public async Task<ActionResult<CustomerReadDto>> CreateCustomer (CustomerCreateDto dto)
        {
            var customer = new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EMailAddress = dto.EMailAddress,
                PhoneNumber = dto.PhoneNumber,
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var readDto = new CustomerReadDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                EMailAddress = customer.EMailAddress,
                PhoneNumber = customer.PhoneNumber,
                Products = customer.Products.ToList()
            };
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, readDto);
        }

        // Read all Customers
        [HttpGet]
        public async Task<ActionResult<CustomerReadDto>> GetCustomers()
        {
            var customers = await _context.Customers.
                AsNoTracking()
                .Select(x => new CustomerCreateDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EMailAddress = x.EMailAddress,
                    PhoneNumber = x.PhoneNumber,
                })
                .ToListAsync();
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }

        // Read single Customer
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerReadDto>> GetCustomer(int id)
        {
            var customer = await _context.Customers
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CustomerReadDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    EMailAddress = c.EMailAddress,
                    PhoneNumber = c.PhoneNumber,
                })
                .FirstOrDefaultAsync();
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // Update Customer
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDto dto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) { return NotFound(); }
            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.PhoneNumber = dto.PhoneNumber;
            customer.EMailAddress = dto.EMailAddress;
            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        //Delete Customer
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) { return NotFound(); };
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }

    }
}
