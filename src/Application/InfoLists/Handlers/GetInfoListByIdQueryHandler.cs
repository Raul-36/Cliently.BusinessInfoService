using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.DTOs.Responses;
using Application.InfoLists.Queries;
using Core.InfoLists.Repositories.Base;
using AutoMapper;
using Application.InfoLists.Exceptions;
using Application.Users.Services.Base;
using System;

namespace Application.InfoLists.Handlers
{
    public class GetInfoListByIdQueryHandler : IRequestHandler<GetInfoListByIdQuery, InfoListResponse>
    {
        private readonly IInfoListRepository infoListRepository;
        private readonly IAccessCheckService accessCheckService;
        private readonly IMapper mapper;


        public GetInfoListByIdQueryHandler(IInfoListRepository infoListRepository, IMapper mapper, IAccessCheckService accessCheckService)
        {
            this.infoListRepository = infoListRepository;
            this.mapper = mapper;
            this.accessCheckService = accessCheckService;
        }

        public async Task<InfoListResponse> Handle(GetInfoListByIdQuery request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToInfoList(request.UserId, request.Id))
            {
                throw new InfoListNotFoundException(request.Id);
            }
            
            var infoList = await infoListRepository.GetByIdAsync(request.Id);
            
            var mappedResponse = mapper.Map<InfoListResponse>(infoList);
            return mappedResponse;
        }
    }
}