using MediatR;
using Application.InfoLists.DTOs.Requests;
using Application.Common;

namespace Application.InfoLists.Commands
{
    public class UpdateInfoListNameCommand : IRequest<Result<string>>
    {
        public required UpdateInfoListNameRequest InfoList { get; set; }
    }
}