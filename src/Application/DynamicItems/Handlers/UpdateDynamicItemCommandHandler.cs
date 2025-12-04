using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.Commands;
using Application.DynamicItems.DTOs.Responses;
using Core.DynamicItems.Repositories.Base;
using Core.DynamicItems.Entities;
using AutoMapper;
using Application.DynamicItems.Exceptions;
using Application.Users.Services.Base;
using System;

namespace Application.DynamicItems.Handlers
{
    public class UpdateDynamicItemCommandHandler : IRequestHandler<UpdateDynamicItemCommand, DynamicItemResponse>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;
        private readonly IAccessCheckService accessCheckService;
        private readonly IMapper mapper;

        public UpdateDynamicItemCommandHandler(IDynamicItemRepository dynamicItemRepository, IMapper mapper, IAccessCheckService accessCheckService)
        {
            this.dynamicItemRepository = dynamicItemRepository;
            this.mapper = mapper;
            this.accessCheckService = accessCheckService;
        }

        public async Task<DynamicItemResponse> Handle(UpdateDynamicItemCommand request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToDynamicItem(request.DynamicItem.UserId, request.DynamicItem.Id))
            {
                throw new DynamicItemNotFoundException(request.DynamicItem.Id);
            }
            
            var dynamicItem = mapper.Map<DynamicItem>(request.DynamicItem);

            var updatedDynamicItem = await dynamicItemRepository.UpdateAsync(dynamicItem);

            if (updatedDynamicItem == null)
                throw new DynamicItemNotFoundException(dynamicItem.Id);
            

            var mappedResponse = mapper.Map<DynamicItemResponse>(updatedDynamicItem);
            return mappedResponse;
        }
    }
}