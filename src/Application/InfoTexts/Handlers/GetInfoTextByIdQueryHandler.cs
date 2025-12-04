using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.DTOs.Responses;
using Application.InfoTexts.Queries;
using Core.InfoTexts.Repositories.Base;
using AutoMapper;
using Application.InfoTexts.Exceptions;
using Application.Users.Services.Base;
using System;

namespace Application.InfoTexts.Handlers
{
    public class GetInfoTextByIdQueryHandler : IRequestHandler<GetInfoTextByIdQuery, InfoTextResponse>
    {
        private readonly IInfoTextRepository infoTextRepository;
        private readonly IAccessCheckService accessCheckService;
        private readonly IMapper mapper;

        public GetInfoTextByIdQueryHandler(IInfoTextRepository infoTextRepository, IMapper mapper, IAccessCheckService accessCheckService)
        {
            this.infoTextRepository = infoTextRepository;
            this.mapper = mapper;
            this.accessCheckService = accessCheckService;
        }

        public async Task<InfoTextResponse> Handle(GetInfoTextByIdQuery request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToInfoText(request.UserId, request.Id))
            {
                throw new InfoTextNotFoundException(request.Id);
            }
            
            var infoText = await infoTextRepository.GetByIdAsync(request.Id);
    
            var mappedResponse = mapper.Map<InfoTextResponse>(infoText);
            return mappedResponse;
        }
    }
}