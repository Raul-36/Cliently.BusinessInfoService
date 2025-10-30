using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.Commands;
using Core.InfoLists.Repositories.Base;
using AutoMapper;

namespace Application.InfoLists.Handlers
{
    public class UpdateInfoListNameCommandHandler : IRequestHandler<UpdateInfoListNameCommand, string>
    {
        private readonly IInfoListRepository infoListRepository;
        private readonly IMapper _mapper;

        public UpdateInfoListNameCommandHandler(IInfoListRepository infoListRepository, IMapper mapper)
        {
            this.infoListRepository = infoListRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(UpdateInfoListNameCommand request, CancellationToken cancellationToken)
        {
            var newName = await infoListRepository.SetNameByIdAsync(request.InfoList.Id, request.InfoList.Name);
            return newName;
        }
    }
}