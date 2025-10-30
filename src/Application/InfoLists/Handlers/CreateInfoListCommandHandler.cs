using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.Commands;
using Application.InfoLists.DTOs.Responses;
using Core.InfoLists.Repositories.Base;
using Core.InfoLists.Models;
using AutoMapper;

namespace Application.InfoLists.Handlers
{
    public class CreateInfoListCommandHandler : IRequestHandler<CreateInfoListCommand, InfoListResponse>
    {
        private readonly IInfoListRepository infoListRepository;
        private readonly IMapper _mapper;

        public CreateInfoListCommandHandler(IInfoListRepository infoListRepository, IMapper mapper)
        {
            this.infoListRepository = infoListRepository;
            _mapper = mapper;
        }

        public async Task<InfoListResponse> Handle(CreateInfoListCommand request, CancellationToken cancellationToken)
        {
            var infoList = _mapper.Map<InfoList>(request.InfoList);

            var createdInfoList = await infoListRepository.AddAsync(infoList);

            return _mapper.Map<InfoListResponse>(createdInfoList);
        }
    }
}