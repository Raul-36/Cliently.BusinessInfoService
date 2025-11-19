using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.Commands;
using Application.InfoTexts.DTOs.Responses;
using Core.InfoTexts.Repositories.Base;
using Core.InfoTexts.Entities;
using AutoMapper;

namespace Application.InfoTexts.Handlers
{
    public class CreateInfoTextCommandHandler : IRequestHandler<CreateInfoTextCommand, InfoTextResponse>
    {
        private readonly IInfoTextRepository infoTextRepository;
        private readonly IMapper mapper;

        public CreateInfoTextCommandHandler(IInfoTextRepository infoTextRepository, IMapper mapper)
        {
            this.infoTextRepository = infoTextRepository;
            this.mapper = mapper;
        }

        public async Task<InfoTextResponse> Handle(CreateInfoTextCommand request, CancellationToken cancellationToken)
        {
            var infoText = this.mapper.Map<InfoText>(request.InfoText);

            var createdInfoText = await infoTextRepository.AddAsync(infoText);

            var mappedResponse = this.mapper.Map<InfoTextResponse>(createdInfoText);
            return mappedResponse;
        }
    }
}