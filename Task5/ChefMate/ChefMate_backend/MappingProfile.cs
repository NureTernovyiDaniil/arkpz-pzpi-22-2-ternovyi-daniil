using AutoMapper;
using ChefMate_backend.Models;

namespace ChefMate_backend
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<MenuItem, MenuItemDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

            CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }
}
