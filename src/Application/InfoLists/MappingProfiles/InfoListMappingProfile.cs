using AutoMapper;
using Application.InfoLists.DTOs.Requests;
using Application.InfoLists.DTOs.Responses;
using Core.InfoLists.Entities;

namespace Application.InfoLists.MappingProfiles
{
    public class InfoListMappingProfile : Profile
    {
        public InfoListMappingProfile()
        {
            CreateMap<CreateInfoListRequest, InfoList>();
            CreateMap<UpdateInfoListRequest, InfoList>();
            CreateMap<InfoList, InfoListResponse>();
        }
    }
}
