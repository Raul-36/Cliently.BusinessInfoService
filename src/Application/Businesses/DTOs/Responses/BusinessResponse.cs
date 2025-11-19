using Application.InfoLists.DTOs.Responses;
using Application.InfoTexts.DTOs.Responses;
using Core.InfoLists.Entities;
using Core.InfoTexts.Entities;

namespace Application.Businesses.DTOs.Responses
{
    public class BusinessResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required IEnumerable<InfoListResponse> Lists { get; set; } = Enumerable.Empty<InfoListResponse>();
        public required IEnumerable<ShortInfoTextResponse> Texts { get; set; } = Enumerable.Empty<ShortInfoTextResponse>();
    }
}
