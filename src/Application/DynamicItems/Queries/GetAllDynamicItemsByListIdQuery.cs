using MediatR;
using System;
using System.Collections.Generic;
using Application.DynamicItems.DTOs.Responses;
using Application.Common;

namespace Application.DynamicItems.Queries
{
    public class GetAllDynamicItemsByListIdQuery : IRequest<Result<IEnumerable<DynamicItemResponse>>>
    {
        public Guid ListId { get; set; }
    }
}