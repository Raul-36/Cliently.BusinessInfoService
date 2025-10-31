using MediatR;
using Application.DynamicItems.DTOs.Requests;
using Application.DynamicItems.DTOs.Responses;
using Application.Common;

namespace Application.DynamicItems.Commands
{
    public class CreateDynamicItemCommand : IRequest<Result<DynamicItemResponse>>
    {
        public required CreateDynamicItemRequest DynamicItem { get; set; }
    }
}