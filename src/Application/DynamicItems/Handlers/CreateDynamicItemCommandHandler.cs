using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.DynamicItems.Commands;
using Application.DynamicItems.DTOs.Responses;
using Core.DynamicItems.Repositories.Base;
using Core.DynamicItems.Models;
using AutoMapper;
using Application.Common;

namespace Application.DynamicItems.Handlers
{
    public class CreateDynamicItemCommandHandler : IRequestHandler<CreateDynamicItemCommand, Result<DynamicItemResponse>>
    {
        private readonly IDynamicItemRepository dynamicItemRepository;
        private readonly IMapper mapper;

        public CreateDynamicItemCommandHandler(IDynamicItemRepository dynamicItemRepository, IMapper mapper)
        {
            this.dynamicItemRepository = dynamicItemRepository;
            this.mapper = mapper;
        }

        public async Task<Result<DynamicItemResponse>> Handle(CreateDynamicItemCommand request, CancellationToken cancellationToken)
        {
            var dynamicItem = this.mapper.Map<DynamicItem>(request.DynamicItem);

            var createdDynamicItem = await dynamicItemRepository.AddAsync(dynamicItem);

            var mappedResponse = this.mapper.Map<DynamicItemResponse>(createdDynamicItem);
            return Result<DynamicItemResponse>.Success(mappedResponse);
        }
    }
}