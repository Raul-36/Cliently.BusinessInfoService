using MediatR;
using Application.InfoLists.DTOs.Requests;
using Application.InfoLists.DTOs.Responses;
using Application.Common;

namespace Application.InfoLists.Commands
{
    public class CreateInfoListCommand : IRequest<Result<InfoListResponse>>
    {
        public required CreateInfoListRequest InfoList { get; set; }
    }
}