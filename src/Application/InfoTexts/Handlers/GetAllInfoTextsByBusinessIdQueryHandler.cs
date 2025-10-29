using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.DTOs.Responses;
using Application.InfoTexts.Queries;
using Core.InfoTexts.Repositories.Base;

namespace Application.InfoTexts.Handlers
{
    public class GetAllInfoTextsByBusinessIdQueryHandler : IRequestHandler<GetAllInfoTextsByBusinessIdQuery, IEnumerable<InfoTextResponse>>
    {
        private readonly IInfoTextRepository infoTextRepository;

        public GetAllInfoTextsByBusinessIdQueryHandler(IInfoTextRepository infoTextRepository)
        {
            this.infoTextRepository = infoTextRepository;
        }

        public async Task<IEnumerable<InfoTextResponse>> Handle(GetAllInfoTextsByBusinessIdQuery request, CancellationToken cancellationToken)
        {
            var infoTexts = await infoTextRepository.GetAllByBusinessIdAsync(request.BusinessId);
            return infoTexts.Select(it => new InfoTextResponse { Id = it.Id, Text = it.Text });
        }
    }
}