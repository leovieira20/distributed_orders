﻿using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using OrderManagement.Domain.Model;
using OrderManagement.Repository.Mongo.Models;

namespace OrderManagement.Repository.Mongo.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(x => x._id, expression => expression.Ignore())
                .ReverseMap();
            CreateMap<Address, AddressDTO>()
                .ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>()
                .ReverseMap();
            CreateMap<OrderStatus, OrderStatusDTO>()
                .ConvertUsingEnumMapping()
                .ReverseMap();
            
            // CreateMap<OrderDTO, Order>();
            // CreateMap<AddressDTO, Address>();
            // CreateMap<OrderItemDTO, OrderItem>();
            // CreateMap<OrderStatusDTO, OrderStatus>()
            //     .ConvertUsingEnumMapping();
        }
    }
}