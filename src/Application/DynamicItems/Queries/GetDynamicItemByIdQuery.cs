using MediatR;
using System;
using Application.DynamicItems.DTOs.Responses;
using Application.Common;

namespace Application.DynamicItems.Queries
{
    public class GetDynamicItemByIdQuery : IRequest<Result<DynamicItemResponse>>
    {
        public Guid Id { get; set; }
    }
}