using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.Commands;
using Application.InfoLists.DTOs.Responses;
using Core.InfoLists.Repositories.Base;
using Core.InfoLists.Models;

namespace Application.InfoLists.Handlers
{
    public class CreateInfoListCommandHandler : IRequestHandler<CreateInfoListCommand, InfoListResponse>
    {
        private readonly IInfoListRepository infoListRepository;

        public CreateInfoListCommandHandler(IInfoListRepository infoListRepository)
        {
            this.infoListRepository = infoListRepository;
        }

        public async Task<InfoListResponse> Handle(CreateInfoListCommand request, CancellationToken cancellationToken)
        {
            var infoList = new InfoList { Name = request.InfoList.Name };

            var createdInfoList = await infoListRepository.AddAsync(infoList);

            return new InfoListResponse
            {
                Id = createdInfoList.Id,
                Name = createdInfoList.Name
            };
        }
    }
}