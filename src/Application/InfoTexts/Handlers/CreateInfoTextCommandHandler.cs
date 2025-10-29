using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.Commands;
using Application.InfoTexts.DTOs.Responses;
using Core.InfoTexts.Repositories.Base;
using Core.InfoTexts.Models;

namespace Application.InfoTexts.Handlers
{
    public class CreateInfoTextCommandHandler : IRequestHandler<CreateInfoTextCommand, InfoTextResponse>
    {
        private readonly IInfoTextRepository infoTextRepository;

        public CreateInfoTextCommandHandler(IInfoTextRepository infoTextRepository)
        {
            this.infoTextRepository = infoTextRepository;
        }

        public async Task<InfoTextResponse> Handle(CreateInfoTextCommand request, CancellationToken cancellationToken)
        {
            var infoText = new InfoText {Name = request.InfoText.Name, Text = request.InfoText.Text };

            var createdInfoText = await infoTextRepository.AddAsync(infoText);

            return new InfoTextResponse
            {
                Id = createdInfoText.Id,
                Text = createdInfoText.Text
            };
        }
    }
}