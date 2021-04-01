using System;

namespace Commerce.Domain.DTOs
{
    public class GetProductsResult
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
