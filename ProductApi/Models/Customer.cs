using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string EMailAddress { get; set; }
        public int? PhoneNumber { get; set; }
        public bool IsValid { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
