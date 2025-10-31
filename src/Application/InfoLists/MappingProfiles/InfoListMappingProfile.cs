using AutoMapper;
using Application.InfoLists.DTOs.Requests;
using Application.InfoLists.DTOs.Responses;
using Core.InfoLists.Models;

namespace Application.InfoLists.MappingProfiles
{
    public class InfoListMappingProfile : Profile
    {
        public InfoListMappingProfile()
        {
            CreateMap<CreateInfoListRequest, InfoList>();
            CreateMap<UpdateInfoListNameRequest, InfoList>();
            CreateMap<InfoList, InfoListResponse>();
        }
    }
}
