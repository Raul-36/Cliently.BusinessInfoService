using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.InfoLists.Commands;
using Core.InfoLists.Repositories.Base;
using AutoMapper;
using Application.Common;

namespace Application.InfoLists.Handlers
{
    public class UpdateInfoListNameCommandHandler : IRequestHandler<UpdateInfoListNameCommand, Result<string>>
    {
        private readonly IInfoListRepository infoListRepository;

        public UpdateInfoListNameCommandHandler(IInfoListRepository infoListRepository)
        {
            this.infoListRepository = infoListRepository;
        }

        public async Task<Result<string>> Handle(UpdateInfoListNameCommand request, CancellationToken cancellationToken)
        {
            var newName = await infoListRepository.SetNameByIdAsync(request.InfoList.Id, request.InfoList.Name);
            if (newName == null)
            {
                return Result<string>.Failure("InfoList not found.");
            }
            return Result<string>.Success(newName);
        }
    }
}