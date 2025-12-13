using System.ComponentModel.DataAnnotations;

namespace ProductApi.DTO
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
