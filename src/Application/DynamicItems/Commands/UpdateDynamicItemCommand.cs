using MediatR;
using Application.DynamicItems.DTOs.Requests;
using Application.DynamicItems.DTOs.Responses;

namespace Application.DynamicItems.Commands
{
    public class UpdateDynamicItemCommand : IRequest<DynamicItemResponse>
    {
        public required UpdateDynamicItemRequest DynamicItem { get; set; }
    }
}