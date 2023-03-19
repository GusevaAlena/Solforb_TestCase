using AutoMapper;
using OrderManagerWebApp.Models;

namespace OrderManagerWebApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderItemViewModel, OrderItem>().ReverseMap();
            CreateMap<OrderViewModel, Order>().ReverseMap();
        }
    }
}
