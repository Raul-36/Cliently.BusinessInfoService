using MediatR;
using System;
using Application.DynamicItems.DTOs.Responses;

namespace Application.DynamicItems.Queries
{
    public class GetDynamicItemByIdQuery : IRequest<DynamicItemResponse>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}