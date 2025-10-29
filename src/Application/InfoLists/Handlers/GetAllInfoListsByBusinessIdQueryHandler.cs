using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.DTOs.Responses;
using Application.InfoLists.Queries;
using Core.InfoLists.Repositories.Base;


namespace Application.InfoLists.Handlers
{
    public class GetAllInfoListsByBusinessIdQueryHandler : IRequestHandler<GetAllInfoListsByBusinessIdQuery, IEnumerable<InfoListResponse>>
    {
        private readonly IInfoListRepository infoListRepository;

        public GetAllInfoListsByBusinessIdQueryHandler(IInfoListRepository infoListRepository)
        {
            this.infoListRepository = infoListRepository;
        }

        public async Task<IEnumerable<InfoListResponse>> Handle(GetAllInfoListsByBusinessIdQuery request, CancellationToken cancellationToken)
        {
            var infoLists = await infoListRepository.GetAllByBusinessIdAsync(request.BusinessId);
            return infoLists.Select(il => new InfoListResponse { Id = il.Id, Name = il.Name });
        }
    }
}