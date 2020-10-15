using AutoMapper;
using ProductInventory.Domain.Model;
using ProductInventory.Repository.Mongo.Models;

namespace ProductInventory.Repository.Mongo.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(x => x._id, expression => expression.Ignore())
                .ReverseMap();
            // CreateMap<ProductDTO, Product>();
        }
    }
}