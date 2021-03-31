using System.ComponentModel.DataAnnotations;

namespace Commerce.Domain.DTOs
{
    public class AddProductRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
