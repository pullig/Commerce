using Commerce.Domain.Enums;
using System;

namespace Commerce.Domain.DTOs
{
    public class GetProductsRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProductOrderBy OrderBy { get; set; }
    }
}
