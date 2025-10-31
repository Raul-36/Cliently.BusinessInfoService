using AutoMapper;
using Application.Businesses.DTOs.Requests;
using Application.Businesses.DTOs.Responses;
using Core.Businesses.Models;

namespace Application.Businesses.MappingProfiles
{
    public class BusinessMappingProfile : Profile
    {
        public BusinessMappingProfile()
        {
            CreateMap<CreateBusinessRequest, Business>();
            CreateMap<UpdateBusinessRequest, Business>();
            CreateMap<Business, BusinessResponse>().ForMember(
                dest => dest.Lists,
                opt => opt.MapFrom(src => src.Lists.ToList())
            ).ForMember(
                dest => dest.Texts,
                opt => opt.MapFrom(src => src.Texts.ToList())
            );
            CreateMap<Business, ShortBusinessResponse>();
        }
    }
}
