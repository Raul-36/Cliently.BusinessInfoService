using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.Commands;
using Core.InfoLists.Repositories.Base;
using AutoMapper;
using Application.InfoLists.Exceptions;

namespace Application.InfoLists.Handlers
{
    public class UpdateInfoListNameCommandHandler : IRequestHandler<UpdateInfoListNameCommand, string>
    {
        private readonly IInfoListRepository infoListRepository;

        public UpdateInfoListNameCommandHandler(IInfoListRepository infoListRepository)
        {
            this.infoListRepository = infoListRepository;
        }

        public async Task<string> Handle(UpdateInfoListNameCommand request, CancellationToken cancellationToken)
        {
            var newName = await infoListRepository.SetNameByIdAsync(request.InfoList.Id, request.InfoList.Name);
            if (newName == null)
            {
                throw new InfoListNotFoundException(request.InfoList.Id);
            }
            return newName;
        }
    }
}