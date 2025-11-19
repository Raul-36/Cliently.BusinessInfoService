using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.DTOs.Responses;
using Application.InfoLists.Queries;
using Core.InfoLists.Repositories.Base;
using AutoMapper;
using Application.InfoLists.Exceptions;

namespace Application.InfoLists.Handlers
{
    public class GetInfoListByIdQueryHandler : IRequestHandler<GetInfoListByIdQuery, InfoListResponse>
    {
        private readonly IInfoListRepository infoListRepository;
        private readonly IMapper mapper;


        public GetInfoListByIdQueryHandler(IInfoListRepository infoListRepository, IMapper mapper)
        {
            this.infoListRepository = infoListRepository;
            this.mapper = mapper;
        }

        public async Task<InfoListResponse> Handle(GetInfoListByIdQuery request, CancellationToken cancellationToken)
        {
            var infoList = await infoListRepository.GetByIdAsync(request.Id);
            if (infoList == null)
            {
                throw new InfoListNotFoundException(request.Id);
            }
            var mappedResponse = this.mapper.Map<InfoListResponse>(infoList);
            return mappedResponse;
        }
    }
}