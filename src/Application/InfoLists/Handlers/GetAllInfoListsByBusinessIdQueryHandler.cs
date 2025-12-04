using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.DTOs.Responses;
using Application.InfoLists.Queries;
using Core.InfoLists.Repositories.Base;
using AutoMapper;
using Application.Users.Services.Base;
using System;
using Application.Businesses.Exceptions;

namespace Application.InfoLists.Handlers
{
    public class GetAllInfoListsByBusinessIdQueryHandler : IRequestHandler<GetAllInfoListsByBusinessIdQuery, IEnumerable<InfoListResponse>>
    {
        private readonly IInfoListRepository infoListRepository;
        private readonly IAccessCheckService accessCheckService;
        private readonly IMapper mapper;

        public GetAllInfoListsByBusinessIdQueryHandler(IInfoListRepository infoListRepository, IMapper mapper, IAccessCheckService accessCheckService)
        {
            this.infoListRepository = infoListRepository;
            this.mapper = mapper;
            this.accessCheckService = accessCheckService;
        }

        public async Task<IEnumerable<InfoListResponse>> Handle(GetAllInfoListsByBusinessIdQuery request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToBusiness(request.UserId, request.BusinessId))
            {
                throw new BusinessNotFoundException(request.BusinessId);
            }
            
            var infoLists = await infoListRepository.GetAllByBusinessIdAsync(request.BusinessId);
            
            var mappedResponse = mapper.Map<IEnumerable<InfoListResponse>>(infoLists);
            return mappedResponse;
        }
    }
}