using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.DTOs.Responses;
using Application.DynamicItems.Queries;
using Core.DynamicItems.Repositories.Base;
using AutoMapper;

namespace Application.DynamicItems.Handlers
{
    public class GetAllDynamicItemsByListIdQueryHandler : IRequestHandler<GetAllDynamicItemsByListIdQuery, IEnumerable<DynamicItemResponse>>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;
        private readonly IMapper mapper;

        public GetAllDynamicItemsByListIdQueryHandler(IDynamicItemRepository dynamicItemRepository, IMapper mapper)
        {
            this.dynamicItemRepository = dynamicItemRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DynamicItemResponse>> Handle(GetAllDynamicItemsByListIdQuery request, CancellationToken cancellationToken)
        {
            var dynamicItems = await dynamicItemRepository.GetAllByListIdAsync(request.ListId);
            var mappedResponses = this.mapper.Map<IEnumerable<DynamicItemResponse>>(dynamicItems);  
            return mappedResponses;
        }
    }
}