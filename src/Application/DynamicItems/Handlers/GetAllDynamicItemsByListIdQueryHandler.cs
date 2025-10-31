using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.DTOs.Responses;
using Application.DynamicItems.Queries;
using Core.DynamicItems.Repositories.Base;
using Application.Common;
using AutoMapper;

namespace Application.DynamicItems.Handlers
{
    public class GetAllDynamicItemsByListIdQueryHandler : IRequestHandler<GetAllDynamicItemsByListIdQuery, Result<IEnumerable<DynamicItemResponse>>>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;
        private readonly IMapper mapper;

        public GetAllDynamicItemsByListIdQueryHandler(IDynamicItemRepository dynamicItemRepository, IMapper mapper)
        {
            this.dynamicItemRepository = dynamicItemRepository;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<DynamicItemResponse>>> Handle(GetAllDynamicItemsByListIdQuery request, CancellationToken cancellationToken)
        {
            var dynamicItems = await dynamicItemRepository.GetAllByListIdAsync(request.ListId);

            var mappedResponse = dynamicItems.Select(di => mapper.Map<DynamicItemResponse>(di));
            return Result<IEnumerable<DynamicItemResponse>>.Success(mappedResponse);
        }
    }
}