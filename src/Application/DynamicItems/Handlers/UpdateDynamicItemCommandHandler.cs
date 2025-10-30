using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.Commands;
using Application.DynamicItems.DTOs.Responses;
using Core.DynamicItems.Repositories.Base;
using Core.DynamicItems.Models;
using AutoMapper;

namespace Application.DynamicItems.Handlers
{
    public class UpdateDynamicItemCommandHandler : IRequestHandler<UpdateDynamicItemCommand, DynamicItemResponse>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;
        private readonly IMapper _mapper;

        public UpdateDynamicItemCommandHandler(IDynamicItemRepository dynamicItemRepository, IMapper mapper)
        {
            this.dynamicItemRepository = dynamicItemRepository;
            _mapper = mapper;
        }

        public async Task<DynamicItemResponse> Handle(UpdateDynamicItemCommand request, CancellationToken cancellationToken)
        {
            var dynamicItem = _mapper.Map<DynamicItem>(request.DynamicItem);

            var updatedDynamicItem = await dynamicItemRepository.UpdateAsync(dynamicItem);

            return _mapper.Map<DynamicItemResponse>(updatedDynamicItem);
        }
    }
}