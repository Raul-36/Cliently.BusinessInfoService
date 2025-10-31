using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.DTOs.Responses;
using Application.DynamicItems.Queries;
using Core.DynamicItems.Repositories.Base;
using Application.Common;
using AutoMapper;

namespace Application.DynamicItems.Handlers
{
    public class GetDynamicItemByIdQueryHandler : IRequestHandler<GetDynamicItemByIdQuery, Result<DynamicItemResponse>>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;
        private readonly IMapper mapper;

        public GetDynamicItemByIdQueryHandler(IDynamicItemRepository dynamicItemRepository, IMapper mapper)
        {
            this.dynamicItemRepository = dynamicItemRepository;
            this.mapper = mapper;
        }

        public async Task<Result<DynamicItemResponse>> Handle(GetDynamicItemByIdQuery request, CancellationToken cancellationToken)
        {
            var dynamicItem = await dynamicItemRepository.GetByIdAsync(request.Id);
            if (dynamicItem == null)
            {
                return Result<DynamicItemResponse>.Failure($"DynamicItem with Id {request.Id} not found.");
            }

            var mappedResponse = this.mapper.Map<DynamicItemResponse>(dynamicItem);
            return Result<DynamicItemResponse>.Success(mappedResponse);
        }
    }
}