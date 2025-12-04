using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.Commands;
using Application.InfoTexts.DTOs.Responses;
using Core.InfoTexts.Repositories.Base;
using Core.InfoTexts.Entities;
using AutoMapper;
using Application.InfoTexts.Exceptions;
using Application.Users.Services.Base;
using System;

namespace Application.InfoTexts.Handlers
{
    public class UpdateInfoTextCommandHandler : IRequestHandler<UpdateInfoTextCommand, InfoTextResponse>
    {
        private readonly IInfoTextRepository infoTextRepository;
        private readonly IAccessCheckService accessCheckService;
        private readonly IMapper mapper;

        public UpdateInfoTextCommandHandler(IInfoTextRepository infoTextRepository, IMapper mapper, IAccessCheckService accessCheckService)
        {
            this.infoTextRepository = infoTextRepository;
            this.mapper = mapper;
            this.accessCheckService = accessCheckService;
        }

        public async Task<InfoTextResponse> Handle(UpdateInfoTextCommand request, CancellationToken cancellationToken)
        {
            if (!accessCheckService.ToInfoText(request.InfoText.UserId, request.InfoText.Id))
            {
                throw new InfoTextNotFoundException(request.InfoText.Id);
            }
            
            var infoText = mapper.Map<InfoText>(request.InfoText);

            var updatedInfoText = await infoTextRepository.UpdateAsync(infoText);

            if (updatedInfoText == null)
            {
                throw new InfoTextNotFoundException(infoText.Id);
            }
            var mappedResponse = mapper.Map<InfoTextResponse>(updatedInfoText);
            return mappedResponse;
        }
    }
}