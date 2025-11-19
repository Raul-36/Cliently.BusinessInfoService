using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoTexts.DTOs.Responses;
using Application.InfoTexts.Queries;
using Core.InfoTexts.Repositories.Base;
using AutoMapper;
using Application.InfoTexts.Exceptions;

namespace Application.InfoTexts.Handlers
{
    public class GetInfoTextByIdQueryHandler : IRequestHandler<GetInfoTextByIdQuery, InfoTextResponse>
    {
        private readonly IInfoTextRepository infoTextRepository;
        private readonly IMapper mapper;

        public GetInfoTextByIdQueryHandler(IInfoTextRepository infoTextRepository, IMapper mapper)
        {
            this.infoTextRepository = infoTextRepository;
            this.mapper = mapper;
        }

        public async Task<InfoTextResponse> Handle(GetInfoTextByIdQuery request, CancellationToken cancellationToken)
        {
            var infoText = await infoTextRepository.GetByIdAsync(request.Id);
            if (infoText == null)
            {
                throw new InfoTextNotFoundException(request.Id);
            }
            var mappedResponse = this.mapper.Map<InfoTextResponse>(infoText);
            return mappedResponse;
        }
    }
}