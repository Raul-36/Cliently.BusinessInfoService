using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.DTOs.Responses;
using Application.InfoTexts.Queries;
using Core.InfoTexts.Repositories.Base;
using Application.Common;
using AutoMapper;

namespace Application.InfoTexts.Handlers
{
    public class GetAllInfoTextsByBusinessIdQueryHandler : IRequestHandler<GetAllInfoTextsByBusinessIdQuery, Result<IEnumerable<InfoTextResponse>>>
    {
        private readonly IInfoTextRepository infoTextRepository;
        private readonly IMapper mapper;

        public GetAllInfoTextsByBusinessIdQueryHandler(IInfoTextRepository infoTextRepository, IMapper mapper)
        {
            this.infoTextRepository = infoTextRepository;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<InfoTextResponse>>> Handle(GetAllInfoTextsByBusinessIdQuery request, CancellationToken cancellationToken)
        {
            var infoTexts = await infoTextRepository.GetAllByBusinessIdAsync(request.BusinessId);

            var mappedResponse = infoTexts.Select(it => mapper.Map<InfoTextResponse>(it));
            return Result<IEnumerable<InfoTextResponse>>.Success(mappedResponse);
        }
    }
}