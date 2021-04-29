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
            CreateMap<User, GetUserResult>();
            CreateMap<User, SignedUser>();
            CreateMap<User, SignInResult>();

            CreateMap<AddProductRequest, Product>();
            CreateMap<UpdateProductRequest, Product>();
            CreateMap<Product, GetProductsResult>();

            CreateMap<AddOrderRequest, Order>();
            CreateMap<AddOrderProductRequest, ProductOrder>();
            CreateMap<Order, GetOrderResult>();
            CreateMap<ProductOrder, GetProductOrdersResult>();
        }
    }
}
