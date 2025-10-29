using MediatR;
using System;
using System.Collections.Generic;
using Application.DynamicItems.DTOs.Responses;

namespace Application.DynamicItems.Queries
{
    public class GetAllDynamicItemsByListIdQuery : IRequest<IEnumerable<DynamicItemResponse>>
    {
        public Guid ListId { get; set; }
    }
}