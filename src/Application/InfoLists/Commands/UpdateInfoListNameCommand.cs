using MediatR;
using Application.InfoLists.DTOs.Requests;

namespace Application.InfoLists.Commands
{
    public class UpdateInfoListNameCommand : IRequest<string>
    {
        public required UpdateInfoListRequest InfoList { get; set; }
    }
}