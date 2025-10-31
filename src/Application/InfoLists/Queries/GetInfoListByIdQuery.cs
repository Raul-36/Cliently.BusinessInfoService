using MediatR;
using System;
using Application.InfoLists.DTOs.Responses;
using Application.Common;

namespace Application.InfoLists.Queries
{
    public class GetInfoListByIdQuery : IRequest<Result<InfoListResponse>>
    {
        public Guid Id { get; set; }
    }
}