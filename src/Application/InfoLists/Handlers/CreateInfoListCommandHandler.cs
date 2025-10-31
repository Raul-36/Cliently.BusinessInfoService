using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.Commands;
using Application.InfoLists.DTOs.Responses;
using Core.InfoLists.Repositories.Base;
using Core.InfoLists.Models;
using AutoMapper;
using Application.Common;

namespace Application.InfoLists.Handlers
{
    public class CreateInfoListCommandHandler : IRequestHandler<CreateInfoListCommand, Result<InfoListResponse>>
    {
        private readonly IInfoListRepository infoListRepository;
        private readonly IMapper mapper;

        public CreateInfoListCommandHandler(IInfoListRepository infoListRepository, IMapper mapper)
        {
            this.infoListRepository = infoListRepository;
            this.mapper = mapper;
        }

        public async Task<Result<InfoListResponse>> Handle(CreateInfoListCommand request, CancellationToken cancellationToken)
        {
            var infoList = this.mapper.Map<InfoList>(request.InfoList);

            var createdInfoList = await infoListRepository.AddAsync(infoList);

            var mappedResponse = this.mapper.Map<InfoListResponse>(createdInfoList);
            return Result<InfoListResponse>.Success(mappedResponse);
        }
    }
}