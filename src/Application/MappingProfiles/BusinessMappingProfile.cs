using AutoMapper;
using Application.Businesses.DTOs.Requests;
using Application.Businesses.DTOs.Responses;
using Core.Businesses.Models;

namespace Application.MappingProfiles
{
    public class BusinessMappingProfile : Profile
    {
        public BusinessMappingProfile()
        {
            CreateMap<CreateBusinessRequest, Business>();
            CreateMap<UpdateBusinessRequest, Business>();
            CreateMap<Business, BusinessResponse>();
        }
    }
}
