using ProductApi.Models;

namespace ProductApi.DTO
{
    public class CustomerUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMailAddress { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
