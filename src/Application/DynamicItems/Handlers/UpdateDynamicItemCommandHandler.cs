using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.Commands;
using Application.DynamicItems.DTOs.Responses;
using Core.DynamicItems.Repositories.Base;
using Core.DynamicItems.Models;

namespace Application.DynamicItems.Handlers
{
    public class UpdateDynamicItemCommandHandler : IRequestHandler<UpdateDynamicItemCommand, DynamicItemResponse>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;

        public UpdateDynamicItemCommandHandler(IDynamicItemRepository dynamicItemRepository)
        {
            this.dynamicItemRepository = dynamicItemRepository;
        }

        public async Task<DynamicItemResponse> Handle(UpdateDynamicItemCommand request, CancellationToken cancellationToken)
        {
            var dynamicItem = new DynamicItem { Id = request.DynamicItem.Id, ListId = request.DynamicItem.ListId, Properties = request.DynamicItem.Properties };

            var updatedDynamicItem = await dynamicItemRepository.UpdateAsync(dynamicItem);

            return new DynamicItemResponse
            {
                Id = updatedDynamicItem.Id,
                ListId = updatedDynamicItem.ListId,
                Properties = updatedDynamicItem.Properties ?? new Dictionary<string, object>()
            };
        }
    }
}