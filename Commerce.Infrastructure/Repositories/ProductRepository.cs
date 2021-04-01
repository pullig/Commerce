using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using Commerce.Domain.Enums;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(CommerceContext context) : base(context)
        {

        }

        public IEnumerable<Product> GetProducts(GetProductsRequest dto)
        {
            IEnumerable<Product> result = context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                result = result.Where(u => u.Name.Contains(dto.Name));
            }

            if (!string.IsNullOrWhiteSpace(dto.Description))
            {
                result = result.Where(u => u.Description.Contains(dto.Description));
            }

            if (dto.Price.HasValue)
            {
                result = result.Where(u => u.Price == dto.Price);
            }

            if (dto.StartDate.HasValue)
            {
                result = result.Where(u => u.CreationDate.Date >= dto.StartDate);
            }

            if (dto.EndDate.HasValue)
            {
                result = result.Where(u => u.CreationDate.Date <= dto.EndDate);
            }

            switch (dto.OrderBy)
            {
                default:
                case ProductOrderBy.NameAscending:
                    return result.OrderBy(o => o.Name);
                case ProductOrderBy.DescriptionAscending:
                    return result.OrderBy(o => o.Description);
                case ProductOrderBy.NameDescending:
                    return result.OrderByDescending(o => o.Name);
                case ProductOrderBy.DescriptionDescending:
                    return result.OrderByDescending(o => o.Name);
            }
        }
    }
}
