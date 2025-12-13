using ProductApi.Models;

namespace ProductApi.DTO
{
    public class CustomerReadDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Product> Products { get; set; }
    }
    }
