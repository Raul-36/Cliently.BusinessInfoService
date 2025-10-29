using MediatR;
using Application.InfoLists.DTOs.Requests;
using Application.InfoLists.DTOs.Responses;

namespace Application.InfoLists.Commands
{
    public class CreateInfoListCommand : IRequest<InfoListResponse>
    {
        public required CreateInfoListRequest InfoList { get; set; }
    }
}