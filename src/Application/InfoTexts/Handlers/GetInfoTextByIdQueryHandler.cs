using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.DTOs.Responses;
using Application.InfoTexts.Queries;
using Core.InfoTexts.Repositories.Base;

namespace Application.InfoTexts.Handlers
{
    public class GetInfoTextByIdQueryHandler : IRequestHandler<GetInfoTextByIdQuery, InfoTextResponse>
    {
        private readonly IInfoTextRepository infoTextRepository;

        public GetInfoTextByIdQueryHandler(IInfoTextRepository infoTextRepository)
        {
            this.infoTextRepository = infoTextRepository;
        }

        public async Task<InfoTextResponse> Handle(GetInfoTextByIdQuery request, CancellationToken cancellationToken)
        {
            var infoText = await infoTextRepository.GetByIdAsync(request.Id);
            if (infoText == null)
            {
                return null; // Or throw an exception, depending on desired behavior
            }
            return new InfoTextResponse { Id = infoText.Id, Text = infoText.Text };
        }
    }
}