using MediatR;
using Application.DynamicItems.DTOs.Requests;
using Application.DynamicItems.DTOs.Responses;

namespace Application.DynamicItems.Commands
{
    public class CreateDynamicItemCommand : IRequest<DynamicItemResponse>
    {
        public required CreateDynamicItemRequest DynamicItem { get; set; }
    }
}