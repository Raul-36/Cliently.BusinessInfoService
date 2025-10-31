using MediatR;
using System;
using System.Collections.Generic;
using Application.InfoLists.DTOs.Responses;
using Application.Common;

namespace Application.InfoLists.Queries
{
    public class GetAllInfoListsByBusinessIdQuery : IRequest<Result<IEnumerable<InfoListResponse>>>
    {
        public Guid BusinessId { get; set; }
    }
}