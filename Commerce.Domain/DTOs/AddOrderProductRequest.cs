using System.ComponentModel.DataAnnotations;

namespace Commerce.Domain.DTOs
{
    public class AddOrderProductRequest
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
