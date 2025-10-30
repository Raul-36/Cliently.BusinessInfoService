using AutoMapper;
using Application.DynamicItems.DTOs.Requests;
using Application.DynamicItems.DTOs.Responses;
using Core.DynamicItems.Models;

namespace Application.MappingProfiles
{
    public class DynamicItemMappingProfile : Profile
    {
        public DynamicItemMappingProfile()
        {
            CreateMap<CreateDynamicItemRequest, DynamicItem>();
            CreateMap<UpdateDynamicItemRequest, DynamicItem>();
            CreateMap<DynamicItem, DynamicItemResponse>();
        }
    }
}
