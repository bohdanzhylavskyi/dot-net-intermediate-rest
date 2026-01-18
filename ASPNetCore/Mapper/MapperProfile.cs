using ASPNetCore.DTOs;
using ASPNetCore.Entities;
using AutoMapper;

namespace ASPNetCore.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CategoryCreationDTO, Category>();
            CreateMap<Category, CategoryDTO>();

            CreateMap<ItemCreationDTO, Item>();
            CreateMap<Item, ItemDTO>();
        }
    }
}
