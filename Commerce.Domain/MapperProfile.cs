using AutoMapper;
using Commerce.Domain.Entities;
using Commerce.Domain.DTOs;

namespace Commerce.Domain
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SignUpRequest, User>();
            CreateMap<User, GetUserAsyncResult>();
            CreateMap<User, SignedUser>();

            CreateMap<AddProductRequest, Product>();
            CreateMap<UpdateProductRequest, Product>();
            CreateMap<Product, GetProductsResult>();

            CreateMap<AddOrderRequest, Order>();
            CreateMap<AddOrderProductRequest, ProductOrder>();
        }
    }
}
