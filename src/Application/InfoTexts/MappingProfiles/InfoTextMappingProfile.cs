using AutoMapper;
using Application.InfoTexts.DTOs.Requests;
using Application.InfoTexts.DTOs.Responses;
using Core.InfoTexts.Models;

namespace Application.InfoTexts.MappingProfiles
{
    public class InfoTextMappingProfile : Profile
    {
        public InfoTextMappingProfile()
        {
            CreateMap<CreateInfoTextRequest, InfoText>();
            CreateMap<UpdateInfoTextRequest, InfoText>();
            CreateMap<InfoText, InfoTextResponse>();
            CreateMap<InfoText, ShortInfoTextResponse>();
        }
    }
}
