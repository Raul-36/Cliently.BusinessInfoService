using MediatR;
using System;
using Application.InfoLists.DTOs.Responses;

namespace Application.InfoLists.Queries
{
    public class GetInfoListByIdQuery : IRequest<InfoListResponse>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}