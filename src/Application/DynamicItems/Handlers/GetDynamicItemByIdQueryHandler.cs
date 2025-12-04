using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.DTOs.Responses;
using Application.DynamicItems.Queries;
using Core.DynamicItems.Repositories.Base;
using AutoMapper;
using Application.DynamicItems.Exceptions;
using Application.Users.Services.Base;
using System;

namespace Application.DynamicItems.Handlers
{
    public class GetDynamicItemByIdQueryHandler : IRequestHandler<GetDynamicItemByIdQuery, DynamicItemResponse>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;
        private readonly IAccessCheckService accessCheckService;
        private readonly IMapper mapper;

        public GetDynamicItemByIdQueryHandler(IDynamicItemRepository dynamicItemRepository, IMapper mapper, IAccessCheckService accessCheckService)
        {
            this.dynamicItemRepository = dynamicItemRepository;
            this.mapper = mapper;
            this.accessCheckService = accessCheckService;
        }

        public async Task<DynamicItemResponse> Handle(GetDynamicItemByIdQuery request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToDynamicItem(request.UserId, request.Id))
            {
                throw new DynamicItemNotFoundException(request.Id);
            }
            
            var dynamicItem = await dynamicItemRepository.GetByIdAsync(request.Id);
            

            var mappedResponse = mapper.Map<DynamicItemResponse>(dynamicItem);
            return mappedResponse;
        }
    }
}