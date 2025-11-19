using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.DTOs.Responses;
using Application.DynamicItems.Queries;
using Core.DynamicItems.Repositories.Base;
using AutoMapper;
using Application.DynamicItems.Exceptions;

namespace Application.DynamicItems.Handlers
{
    public class GetDynamicItemByIdQueryHandler : IRequestHandler<GetDynamicItemByIdQuery, DynamicItemResponse>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;
        private readonly IMapper mapper;

        public GetDynamicItemByIdQueryHandler(IDynamicItemRepository dynamicItemRepository, IMapper mapper)
        {
            this.dynamicItemRepository = dynamicItemRepository;
            this.mapper = mapper;
        }

        public async Task<DynamicItemResponse> Handle(GetDynamicItemByIdQuery request, CancellationToken cancellationToken)
        {
            var dynamicItem = await dynamicItemRepository.GetByIdAsync(request.Id);
            if (dynamicItem == null)
                throw new DynamicItemNotFoundException(request.Id);

            var mappedResponse = this.mapper.Map<DynamicItemResponse>(dynamicItem);
            return mappedResponse;
        }
    }
}