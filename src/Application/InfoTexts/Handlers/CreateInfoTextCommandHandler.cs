using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.Commands;
using Application.InfoTexts.DTOs.Responses;
using Core.InfoTexts.Repositories.Base;
using Core.InfoTexts.Entities;
using AutoMapper;
using Application.Users.Services.Base;
using System;
using Application.Businesses.Exceptions;

namespace Application.InfoTexts.Handlers
{
    public class CreateInfoTextCommandHandler : IRequestHandler<CreateInfoTextCommand, InfoTextResponse>
    {
        private readonly IInfoTextRepository infoTextRepository;
        private readonly IAccessCheckService accessCheckService;
        private readonly IMapper mapper;

        public CreateInfoTextCommandHandler(IInfoTextRepository infoTextRepository, IMapper mapper, IAccessCheckService accessCheckService)
        {
            this.infoTextRepository = infoTextRepository;
            this.mapper = mapper;
            this.accessCheckService = accessCheckService;
        }

        public async Task<InfoTextResponse> Handle(CreateInfoTextCommand request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToBusiness(request.InfoText.UserId, request.InfoText.BusinessId))
            {
                throw new BusinessNotFoundException(request.InfoText.BusinessId);
            }

            var infoText = mapper.Map<InfoText>(request.InfoText);

            var createdInfoText = await infoTextRepository.AddAsync(infoText);

            var mappedResponse = mapper.Map<InfoTextResponse>(createdInfoText);
            return mappedResponse;
        }
    }
}