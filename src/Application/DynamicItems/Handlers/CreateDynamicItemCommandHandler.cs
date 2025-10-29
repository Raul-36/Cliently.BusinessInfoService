using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.Commands;
using Application.DynamicItems.DTOs.Responses;
using Core.DynamicItems.Repositories.Base;
using Core.DynamicItems.Models;

namespace Application.DynamicItems.Handlers
{
    public class CreateDynamicItemCommandHandler : IRequestHandler<CreateDynamicItemCommand, DynamicItemResponse>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;

        public CreateDynamicItemCommandHandler(IDynamicItemRepository dynamicItemRepository)
        {
            this.dynamicItemRepository = dynamicItemRepository;
        }

        public async Task<DynamicItemResponse> Handle(CreateDynamicItemCommand request, CancellationToken cancellationToken)
        {
            var dynamicItem = new DynamicItem {Name = request.DynamicItem.Name, ListId = request.DynamicItem.ListId, Properties = request.DynamicItem.Properties };

            var createdDynamicItem = await dynamicItemRepository.AddAsync(dynamicItem);

            return new DynamicItemResponse
            {
                Id = createdDynamicItem.Id,
                ListId = createdDynamicItem.ListId,
                Properties = createdDynamicItem.Properties
            };
        }
    }
}