using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.Commands;
using Application.InfoTexts.DTOs.Responses;
using Core.InfoTexts.Repositories.Base;
using Core.InfoTexts.Models;

namespace Application.InfoTexts.Handlers
{
    public class UpdateInfoTextCommandHandler : IRequestHandler<UpdateInfoTextCommand, InfoTextResponse>
    {
        private readonly IInfoTextRepository infoTextRepository;

        public UpdateInfoTextCommandHandler(IInfoTextRepository infoTextRepository)
        {
            this.infoTextRepository = infoTextRepository;
        }

        public async Task<InfoTextResponse> Handle(UpdateInfoTextCommand request, CancellationToken cancellationToken)
        {
            var infoText = new InfoText { Id = request.InfoText.Id, Text = request.InfoText.Text };

            var updatedInfoText = await infoTextRepository.UpdateAsync(infoText);

            return new InfoTextResponse
            {
                Id = updatedInfoText.Id,
                Text = updatedInfoText.Text
            };
        }
    }
}