using MediatR;
using System;
using System.Collections.Generic;
using Application.InfoLists.DTOs.Responses;

namespace Application.InfoLists.Queries
{
    public class GetAllInfoListsByBusinessIdQuery : IRequest<IEnumerable<InfoListResponse>>
    {
        public Guid BusinessId { get; set; }
    }
}