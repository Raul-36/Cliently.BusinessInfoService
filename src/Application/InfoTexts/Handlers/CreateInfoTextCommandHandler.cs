using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.Commands;
using Application.InfoTexts.DTOs.Responses;
using Core.InfoTexts.Repositories.Base;
using Core.InfoTexts.Models;
using AutoMapper;

namespace Application.InfoTexts.Handlers
{
    public class CreateInfoTextCommandHandler : IRequestHandler<CreateInfoTextCommand, InfoTextResponse>
    {
        private readonly IInfoTextRepository infoTextRepository;
        private readonly IMapper _mapper;

        public CreateInfoTextCommandHandler(IInfoTextRepository infoTextRepository, IMapper mapper)
        {
            this.infoTextRepository = infoTextRepository;
            _mapper = mapper;
        }

        public async Task<InfoTextResponse> Handle(CreateInfoTextCommand request, CancellationToken cancellationToken)
        {
            var infoText = _mapper.Map<InfoText>(request.InfoText);

            var createdInfoText = await infoTextRepository.AddAsync(infoText);

            return _mapper.Map<InfoTextResponse>(createdInfoText);
        }
    }
}