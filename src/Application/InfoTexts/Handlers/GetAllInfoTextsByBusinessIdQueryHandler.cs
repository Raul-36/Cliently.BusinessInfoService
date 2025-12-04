using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.DTOs.Responses;
using Application.InfoTexts.Queries;
using Core.InfoTexts.Repositories.Base;
using AutoMapper;
using Application.Users.Services.Base;
using System;
using Application.Businesses.Exceptions;

namespace Application.InfoTexts.Handlers
{
    public class GetAllInfoTextsByBusinessIdQueryHandler : IRequestHandler<GetAllInfoTextsByBusinessIdQuery, IEnumerable<InfoTextResponse>>
    {
        private readonly IInfoTextRepository infoTextRepository;
        private readonly IAccessCheckService accessCheckService;
        private readonly IMapper mapper;

        public GetAllInfoTextsByBusinessIdQueryHandler(IInfoTextRepository infoTextRepository, IMapper mapper, IAccessCheckService accessCheckService)
        {
            this.infoTextRepository = infoTextRepository;
            this.mapper = mapper;
            this.accessCheckService = accessCheckService;
        }

        public async Task<IEnumerable<InfoTextResponse>> Handle(GetAllInfoTextsByBusinessIdQuery request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToBusiness(request.UserId, request.BusinessId))
            {
                throw new BusinessNotFoundException(request.BusinessId);
            }
            
            var infoTexts = await infoTextRepository.GetAllByBusinessIdAsync(request.BusinessId);

            var mappedResponse = mapper.Map<IEnumerable<InfoTextResponse>>(infoTexts);
            return mappedResponse;
        }
    }
}