using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.Commands;
using Application.InfoTexts.DTOs.Responses;
using Core.InfoTexts.Repositories.Base;
using Core.InfoTexts.Entities;
using AutoMapper;
using Application.InfoTexts.Exceptions;

namespace Application.InfoTexts.Handlers
{
    public class UpdateInfoTextCommandHandler : IRequestHandler<UpdateInfoTextCommand, InfoTextResponse>
    {
        private readonly IInfoTextRepository infoTextRepository;
        private readonly IMapper mapper;

        public UpdateInfoTextCommandHandler(IInfoTextRepository infoTextRepository, IMapper mapper)
        {
            this.infoTextRepository = infoTextRepository;
            this.mapper = mapper;
        }

        public async Task<InfoTextResponse> Handle(UpdateInfoTextCommand request, CancellationToken cancellationToken)
        {
            var infoText = this.mapper.Map<InfoText>(request.InfoText);

            var updatedInfoText = await infoTextRepository.UpdateAsync(infoText);

            if (updatedInfoText == null)
            {
                throw new InfoTextNotFoundException(infoText.Id);
            }
            var mappedResponse = this.mapper.Map<InfoTextResponse>(updatedInfoText);
            return mappedResponse;
        }
    }
}