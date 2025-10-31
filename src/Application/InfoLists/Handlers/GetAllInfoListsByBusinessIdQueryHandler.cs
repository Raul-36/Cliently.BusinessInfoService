using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.DTOs.Responses;
using Application.InfoLists.Queries;
using Core.InfoLists.Repositories.Base;
using Application.Common;
using AutoMapper;

namespace Application.InfoLists.Handlers
{
    public class GetAllInfoListsByBusinessIdQueryHandler : IRequestHandler<GetAllInfoListsByBusinessIdQuery, Result<IEnumerable<InfoListResponse>>>
    {
        private readonly IInfoListRepository infoListRepository;
        private readonly IMapper mapper;

        public GetAllInfoListsByBusinessIdQueryHandler(IInfoListRepository infoListRepository, IMapper mapper)
        {
            this.infoListRepository = infoListRepository;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<InfoListResponse>>> Handle(GetAllInfoListsByBusinessIdQuery request, CancellationToken cancellationToken)
        {
            var infoLists = await infoListRepository.GetAllByBusinessIdAsync(request.BusinessId);
            
            var mappedResponse = infoLists.Select(il => mapper.Map<InfoListResponse>(il));
            return Result<IEnumerable<InfoListResponse>>.Success(mappedResponse);
        }
    }
}