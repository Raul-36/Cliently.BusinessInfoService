using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.DTOs.Responses;
using Application.InfoTexts.Queries;
using Core.InfoTexts.Repositories.Base;
using AutoMapper;

namespace Application.InfoTexts.Handlers
{
    public class GetAllInfoTextsByBusinessIdQueryHandler : IRequestHandler<GetAllInfoTextsByBusinessIdQuery, IEnumerable<InfoTextResponse>>
    {
        private readonly IInfoTextRepository infoTextRepository;
        private readonly IMapper mapper;

        public GetAllInfoTextsByBusinessIdQueryHandler(IInfoTextRepository infoTextRepository, IMapper mapper)
        {
            this.infoTextRepository = infoTextRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<InfoTextResponse>> Handle(GetAllInfoTextsByBusinessIdQuery request, CancellationToken cancellationToken)
        {
            var infoTexts = await infoTextRepository.GetAllByBusinessIdAsync(request.BusinessId);

            var mappedResponse = mapper.Map<IEnumerable<InfoTextResponse>>(infoTexts);
            return mappedResponse;
        }
    }
}