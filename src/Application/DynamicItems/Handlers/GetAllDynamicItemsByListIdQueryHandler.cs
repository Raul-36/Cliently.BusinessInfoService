using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.DTOs.Responses;
using Application.DynamicItems.Queries;
using Core.DynamicItems.Repositories.Base;

namespace Application.DynamicItems.Handlers
{
    public class GetAllDynamicItemsByListIdQueryHandler : IRequestHandler<GetAllDynamicItemsByListIdQuery, IEnumerable<DynamicItemResponse>>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;

        public GetAllDynamicItemsByListIdQueryHandler(IDynamicItemRepository dynamicItemRepository)
        {
            this.dynamicItemRepository = dynamicItemRepository;
        }

        public async Task<IEnumerable<DynamicItemResponse>> Handle(GetAllDynamicItemsByListIdQuery request, CancellationToken cancellationToken)
        {
            var dynamicItems = await dynamicItemRepository.GetAllByListIdAsync(request.ListId);
            return dynamicItems.Select(di => new DynamicItemResponse { Id = di.Id, ListId = di.ListId, Properties = di.Properties ?? new Dictionary<string, object>() });
        }
    }
}