using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.DTOs.Responses;
using Application.DynamicItems.Queries;
using Core.DynamicItems.Repositories.Base;
using AutoMapper;
using Application.Users.Services.Base;
using System;
using Application.InfoLists.Exceptions;

namespace Application.DynamicItems.Handlers
{
    public class GetAllDynamicItemsByListIdQueryHandler : IRequestHandler<GetAllDynamicItemsByListIdQuery, IEnumerable<DynamicItemResponse>>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;
        private readonly IAccessCheckService accessCheckService;
        private readonly IMapper mapper;

        public GetAllDynamicItemsByListIdQueryHandler(IDynamicItemRepository dynamicItemRepository, IMapper mapper, IAccessCheckService accessCheckService)
        {
            this.dynamicItemRepository = dynamicItemRepository;
            this.mapper = mapper;
            this.accessCheckService = accessCheckService;
        }

        public async Task<IEnumerable<DynamicItemResponse>> Handle(GetAllDynamicItemsByListIdQuery request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToInfoList(request.UserId, request.ListId))
            {
                throw new InfoListNotFoundException(request.ListId);
            }
            
            var dynamicItems = await dynamicItemRepository.GetAllByListIdAsync(request.ListId);
            var mappedResponses = mapper.Map<IEnumerable<DynamicItemResponse>>(dynamicItems);  
            return mappedResponses;
        }
    }
}