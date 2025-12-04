using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.Commands;
using Application.InfoLists.DTOs.Responses;
using Core.InfoLists.Repositories.Base;
using Core.InfoLists.Entities;
using AutoMapper;
using Application.Users.Services.Base;
using System;
using Application.Businesses.Exceptions;

namespace Application.InfoLists.Handlers
{
    public class CreateInfoListCommandHandler : IRequestHandler<CreateInfoListCommand, InfoListResponse>
    {
        private readonly IInfoListRepository infoListRepository;
        private readonly IAccessCheckService accessCheckService;
        private readonly IMapper mapper;

        public CreateInfoListCommandHandler(IInfoListRepository infoListRepository, IMapper mapper, IAccessCheckService accessCheckService)
        {
            this.infoListRepository = infoListRepository;
            this.mapper = mapper;
            this.accessCheckService = accessCheckService;
        }

        public async Task<InfoListResponse> Handle(CreateInfoListCommand request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToBusiness(request.InfoList.UserId, request.InfoList.BusinessId))
            {
                throw new BusinessNotFoundException(request.InfoList.BusinessId);
            }
            
            var infoList = mapper.Map<InfoList>(request.InfoList);

            var createdInfoList = await infoListRepository.AddAsync(infoList);

            var mappedResponse = mapper.Map<InfoListResponse>(createdInfoList);
            return mappedResponse;
        }
    }
}