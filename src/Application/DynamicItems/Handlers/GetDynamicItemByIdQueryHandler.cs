using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.DTOs.Responses;
using Application.DynamicItems.Queries;
using Core.DynamicItems.Repositories.Base;

namespace Application.DynamicItems.Handlers
{
    public class GetDynamicItemByIdQueryHandler : IRequestHandler<GetDynamicItemByIdQuery, DynamicItemResponse>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;

        public GetDynamicItemByIdQueryHandler(IDynamicItemRepository dynamicItemRepository)
        {
            this.dynamicItemRepository = dynamicItemRepository;
        }

        public async Task<DynamicItemResponse> Handle(GetDynamicItemByIdQuery request, CancellationToken cancellationToken)
        {
            var dynamicItem = await dynamicItemRepository.GetByIdAsync(request.Id);
            if (dynamicItem == null)
            {
               throw new KeyNotFoundException($"DynamicItem with Id {request.Id} not found.");
            }
            return new DynamicItemResponse { Id = dynamicItem.Id, ListId = dynamicItem.ListId, Properties = dynamicItem.Properties ?? new Dictionary<string, object>() };
        }
    }
}