using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.DTOs.Responses;
using Application.InfoLists.Queries;
using Core.InfoLists.Repositories.Base;

namespace Application.InfoLists.Handlers
{
    public class GetInfoListByIdQueryHandler : IRequestHandler<GetInfoListByIdQuery, InfoListResponse>
    {
        private readonly IInfoListRepository infoListRepository;

        public GetInfoListByIdQueryHandler(IInfoListRepository infoListRepository)
        {
            this.infoListRepository = infoListRepository;
        }

        public async Task<InfoListResponse> Handle(GetInfoListByIdQuery request, CancellationToken cancellationToken)
        {
            var infoList = await infoListRepository.GetByIdAsync(request.Id);
            if (infoList == null)
            {
                return null; // Or throw an exception
            }
            return new InfoListResponse { Id = infoList.Id, Name = infoList.Name };
        }
    }
}