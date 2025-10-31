using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.DTOs.Responses;
using Application.InfoLists.Queries;
using Core.InfoLists.Repositories.Base;
using Application.Common;
using AutoMapper;

namespace Application.InfoLists.Handlers
{
    public class GetInfoListByIdQueryHandler : IRequestHandler<GetInfoListByIdQuery, Result<InfoListResponse>>
    {
        private readonly IInfoListRepository infoListRepository;
        private readonly IMapper mapper;


        public GetInfoListByIdQueryHandler(IInfoListRepository infoListRepository, IMapper mapper)
        {
            this.infoListRepository = infoListRepository;
            this.mapper = mapper;
        }

        public async Task<Result<InfoListResponse>> Handle(GetInfoListByIdQuery request, CancellationToken cancellationToken)
        {
            var infoList = await infoListRepository.GetByIdAsync(request.Id);
            if (infoList == null)
            {
                return Result<InfoListResponse>.Failure("InfoList not found.");
            }
            var mappedResponse = this.mapper.Map<InfoListResponse>(infoList);
            return Result<InfoListResponse>.Success(mappedResponse);
        }
    }
}