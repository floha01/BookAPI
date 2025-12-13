using System.ComponentModel.DataAnnotations;

namespace ProductApi.DTO
{
    public class ProductUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
