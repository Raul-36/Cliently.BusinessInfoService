using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.Commands;
using Application.DynamicItems.DTOs.Responses;
using Core.DynamicItems.Repositories.Base;
using Core.DynamicItems.Entities;
using AutoMapper;
using Application.Users.Services.Base;
using System;
using Application.InfoLists.Exceptions;

namespace Application.DynamicItems.Handlers
{
    public class CreateDynamicItemCommandHandler : IRequestHandler<CreateDynamicItemCommand, DynamicItemResponse>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;
        private readonly IAccessCheckService accessCheckService;
        private readonly IMapper mapper;

        public CreateDynamicItemCommandHandler(IDynamicItemRepository dynamicItemRepository, IMapper mapper, IAccessCheckService accessCheckService)
        {
            this.dynamicItemRepository = dynamicItemRepository;
            this.mapper = mapper;
            this.accessCheckService = accessCheckService;
        }

        public async Task<DynamicItemResponse> Handle(CreateDynamicItemCommand request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToInfoList(request.DynamicItem.UserId, request.DynamicItem.ListId))
            {
                throw new InfoListNotFoundException(request.DynamicItem.ListId);
            }
            
            var dynamicItem = mapper.Map<DynamicItem>(request.DynamicItem);

            var createdDynamicItem = await dynamicItemRepository.AddAsync(dynamicItem);

            var mappedResponse = mapper.Map<DynamicItemResponse>(createdDynamicItem);
            return mappedResponse;
        }
    }
}