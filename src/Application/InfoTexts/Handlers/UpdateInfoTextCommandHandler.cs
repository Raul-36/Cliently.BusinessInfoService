using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.Commands;
using Application.InfoTexts.DTOs.Responses;
using Core.InfoTexts.Repositories.Base;
using Core.InfoTexts.Models;
using AutoMapper;
using Application.Common;

namespace Application.InfoTexts.Handlers
{
    public class UpdateInfoTextCommandHandler : IRequestHandler<UpdateInfoTextCommand, Result<InfoTextResponse>>
    {
        private readonly IInfoTextRepository infoTextRepository;
        private readonly IMapper mapper;

        public UpdateInfoTextCommandHandler(IInfoTextRepository infoTextRepository, IMapper mapper)
        {
            this.infoTextRepository = infoTextRepository;
            this.mapper = mapper;
        }

        public async Task<Result<InfoTextResponse>> Handle(UpdateInfoTextCommand request, CancellationToken cancellationToken)
        {
            var infoText = this.mapper.Map<InfoText>(request.InfoText);

            var updatedInfoText = await infoTextRepository.UpdateAsync(infoText);

            if (updatedInfoText == null)
            {
                return Result<InfoTextResponse>.Failure("InfoText not found.");
            }
            var mappedResponse = this.mapper.Map<InfoTextResponse>(updatedInfoText);
            return Result<InfoTextResponse>.Success(mappedResponse);
        }
    }
}