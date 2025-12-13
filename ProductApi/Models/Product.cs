using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage="Title must be 10 characters or less"), MinLength(5)]
        public string Title{ get; set; }

        [MaxLength(20, ErrorMessage = "Description must be 10 characters or less"), MinLength(5)]
        public string Description{ get; set; }
        public decimal Price { get; set; }
    }
}
