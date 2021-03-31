using AutoMapper;
using Commerce.Domain.Entities;
using Commerce.Domain.DTOs;

namespace Commerce.Domain
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SignUpDto, User>();
            CreateMap<User, GetUserAsyncResult>();
        }
    }
}
