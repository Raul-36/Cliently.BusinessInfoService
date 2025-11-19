using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.Commands;
using Application.DynamicItems.DTOs.Responses;
using Core.DynamicItems.Repositories.Base;
using Core.DynamicItems.Entities;
using AutoMapper;
using Application.DynamicItems.Exceptions;

namespace Application.DynamicItems.Handlers
{
    public class UpdateDynamicItemCommandHandler : IRequestHandler<UpdateDynamicItemCommand, DynamicItemResponse>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;
        private readonly IMapper mapper;

        public UpdateDynamicItemCommandHandler(IDynamicItemRepository dynamicItemRepository, IMapper mapper)
        {
            this.dynamicItemRepository = dynamicItemRepository;
            this.mapper = mapper;
        }

        public async Task<DynamicItemResponse> Handle(UpdateDynamicItemCommand request, CancellationToken cancellationToken)
        {
            var dynamicItem = this.mapper.Map<DynamicItem>(request.DynamicItem);

            var updatedDynamicItem = await dynamicItemRepository.UpdateAsync(dynamicItem);

            if (updatedDynamicItem == null)
                throw new DynamicItemNotFoundException(dynamicItem.Id);
            

            var mappedResponse = this.mapper.Map<DynamicItemResponse>(updatedDynamicItem);
            return mappedResponse;
        }
    }
}