using MediatR;
using Application.DynamicItems.DTOs.Requests;
using Application.DynamicItems.DTOs.Responses;
using Application.Common;

namespace Application.DynamicItems.Commands
{
    public class UpdateDynamicItemCommand : IRequest<Result<DynamicItemResponse>>
    {
        public required UpdateDynamicItemRequest DynamicItem { get; set; }
    }
}